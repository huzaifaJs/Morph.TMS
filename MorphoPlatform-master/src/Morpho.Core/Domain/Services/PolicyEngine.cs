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
            foreach (var rule in policy.Rules)
            {
                if (rule.SensorType != telemetry.SensorType)
                    continue;

                if (IsViolation(rule, telemetry.Value))
                {
                    return Task.FromResult<PolicyViolation?>(new PolicyViolation(
                        telemetry.TenantId,
                        policy.Id,
                        rule.Id,
                        telemetry.DeviceId,
                        telemetry.SensorType,
                        telemetry.Value,
                        telemetry.Unit,
                        telemetry.Timestamp,
                        telemetry.ShipmentId,
                        telemetry.ContainerId
                    ));
                }
            }

            return Task.FromResult<PolicyViolation?>(null);
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
