using Morpho.Domain.Enums;

namespace Morpho.Integration.MorphoApi
{
    public static class StatusExtensions
    {
        public static DeviceStatusType ToDeviceStatusType(this string status)
        {
            if (string.IsNullOrWhiteSpace(status))
                return DeviceStatusType.Unknown;

            switch (status.Trim().ToLower())
            {
                case "online":
                case "connected":
                case "active":
                    return DeviceStatusType.Connected;

                case "offline":
                case "disconnected":
                case "inactive":
                    return DeviceStatusType.Disconnected;

                case "sleeping":
                case "sleep":
                    return DeviceStatusType.Sleeping;

                case "error":
                case "fault":
                case "failure":
                    return DeviceStatusType.Error;

                default:
                    return DeviceStatusType.Unknown;
            }
        }
    }
}
