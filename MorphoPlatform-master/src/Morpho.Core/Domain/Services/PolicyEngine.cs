using System;
using System.Threading.Tasks;
using Morpho.Domain.Entities.Policies;
using Morpho.Domain.Entities.Telemetry;
using Morpho.Domain.Enums;
using Morpho.Domain.Interfaces;

namespace Morpho.Domain.Services
{
    public class PolicyEngine : IPolicyEngine
    {
        public Task<PolicyViolation?> EvaluateAsync(Policy policy, TelemetryRecord telemetry)
        {
            if (policy == null || telemetry == null)
                return Task.FromResult<PolicyViolation?>(null);

            foreach (var rule in policy.Rules)
            {
                // Extract the correct sensor value from the TelemetryRecord
                decimal? sensorValue = rule.SensorType switch
                {
                    SensorType.Temperature => (decimal?)telemetry.Temperature,
                    SensorType.Humidity => (decimal?)telemetry.Humidity,
                    SensorType.Light => (decimal?)telemetry.Light,
                    SensorType.Vibration => (decimal?)telemetry.MeanVibration,
                    SensorType.BatteryLevel => (decimal?)telemetry.BatteryLevel,
                    SensorType.Rssi => (decimal?)telemetry.Rssi,
                    _ => null
                };

                // If telemetry doesn't include this sensor → skip
                if (sensorValue == null)
                    continue;

                // Threshold comparison
                if (IsViolation(rule, sensorValue.Value))
                {
                    var violation = new PolicyViolation(
                        tenantId: telemetry.TenantId,
                        policyId: policy.Id,
                        ruleId: rule.Id,
                        deviceId: telemetry.DeviceId,
                        sensorType: rule.SensorType,
                        value: sensorValue.Value,
                        unit: GetUnit(rule.SensorType),
                        occurredAtUtc: telemetry.TimestampUtc,
                        shipmentId: telemetry.ShipmentId,
                        containerId: telemetry.ContainerId
                    );

                    return Task.FromResult<PolicyViolation?>(violation);
                }
            }

            return Task.FromResult<PolicyViolation?>(null);
        }
        private string GetUnit(SensorType type)
        {
            return type switch
            {
                SensorType.Temperature => "C",
                SensorType.Humidity => "%",
                SensorType.Light => "lx",
                SensorType.Vibration => "g",
                SensorType.BatteryLevel => "%",
                SensorType.Rssi => "dBm",
                _ => ""
            };
        }


        private bool IsViolation(PolicyRule rule, decimal value)
        {
            var t = rule.Threshold;

            return rule.ConditionType switch
            {
                PolicyConditionType.Range =>
                    (t.Min.HasValue && value < t.Min.Value) ||
                    (t.Max.HasValue && value > t.Max.Value),

                PolicyConditionType.GreaterThan =>
                    (t.Min.HasValue && value > t.Min.Value),

                PolicyConditionType.LessThan =>
                    (t.Max.HasValue && value < t.Max.Value),

                _ => false
            };
        }
    }
}
