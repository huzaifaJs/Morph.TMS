using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Morpho.Domain.Entities.Shipments;
using Morpho.Domain.ValueObjects;
using System;

namespace Morpho.Domain.Entities.Telemetry
{
    public class TelemetryRecord : FullAuditedEntity<Guid>, IMustHaveTenant
    {
        public int TenantId { get; set; }

        // Device relationship (internal IoTDevice GUID)
        public Guid DeviceId { get; protected set; }

        // Shipment relationship
        public Guid? ShipmentId { get; protected set; }
        public Guid? ContainerId { get; protected set; }

        // Timestamps (sent by device)
        public long TimestampRaw { get; protected set; }          // UNIX seconds from device
        public DateTime TimestampUtc { get; protected set; }      // Converted to UTC

        // Firmware / network
        public string FirmwareVersion { get; protected set; }
        public string IpAddress { get; protected set; }

        // GPS (owned value object)
        public GpsLocation Gps { get; protected set; }

        // Sensor data (Morpho)
        public double Rssi { get; protected set; }
        public double BatteryLevel { get; protected set; }
        public double Temperature { get; protected set; }
        public double Humidity { get; protected set; }
        public double MeanVibration { get; protected set; }
        public double Light { get; protected set; }

        // Device state
        public string Status { get; protected set; }

        // RFID count
        public int Nbrfid { get; protected set; }

        // NEW — required fields from client API
        public string DeviceState { get; protected set; }     // ex: "moving", "stopped"
        public string DeviceMode { get; protected set; }      // ex: "auto", "manual"
        public string ConnectionType { get; protected set; }  // "LTE", "WIFI", etc.
        public string SignalQuality { get; protected set; }   // ex: "excellent", "weak"

        // NEW — additional telemetry fields seen in payload
        public double? Pressure { get; protected set; }
        public double? Co2 { get; protected set; }
        public double? Voc { get; protected set; }
        public double? Speed { get; protected set; }
        public double? Altitude { get; protected set; }       // if GPS provides it
        public double? Accuracy { get; protected set; }       // if GPS provides it

        protected TelemetryRecord() { }

        public TelemetryRecord(
            int tenantId,
            Guid deviceId,
            long timestamp,
            string firmwareVersion,
            string ip,
            GpsLocation gps,
            double rssi,
            double battery,
            double temp,
            double humidity,
            double vibration,
            double light,
            string status,
            int nbrfid,
            string deviceState = null,
            string deviceMode = null,
            string connectionType = null,
            string signalQuality = null,
            double? pressure = null,
            double? co2 = null,
            double? voc = null,
            double? speed = null,
            double? altitude = null,
            double? accuracy = null
        )
        {
            TenantId = tenantId;
            DeviceId = deviceId;

            TimestampRaw = timestamp;
            TimestampUtc = DateTimeOffset.FromUnixTimeSeconds(timestamp).UtcDateTime;

            FirmwareVersion = firmwareVersion;
            IpAddress = ip;

            Gps = gps;

            Rssi = rssi;
            BatteryLevel = battery;
            Temperature = temp;
            Humidity = humidity;
            MeanVibration = vibration;
            Light = light;

            Status = status;
            Nbrfid = nbrfid;

            DeviceState = deviceState;
            DeviceMode = deviceMode;
            ConnectionType = connectionType;
            SignalQuality = signalQuality;

            Pressure = pressure;
            Co2 = co2;
            Voc = voc;
            Speed = speed;
            Altitude = altitude;
            Accuracy = accuracy;
        }

        public void LinkShipment(Guid shipmentId, Guid? containerId)
        {
            ShipmentId = shipmentId;
            ContainerId = containerId;
        }
    }
}
