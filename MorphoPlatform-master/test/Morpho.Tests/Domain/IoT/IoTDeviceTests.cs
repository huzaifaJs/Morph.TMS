using Morpho.Domain.Entities.IoT;
using Morpho.Domain.Enums;
using Morpho.Domain.ValueObjects;
using Shouldly;
using System;
using Xunit;

namespace Morpho.Tests.Domain.IoT
{
    public class IoTDeviceTests
    {
        [Fact]
        public void Should_Update_Last_Known_Location()
        {
            var device = new IoTDevice(1, 7777, "SN1", "Temp Sensor", "sensor");

            device.SetLastKnownLocation(26.23, 92.34);

            device.LastKnownLocation.ShouldNotBeNull();
            device.LastKnownLocation.Latitude.ShouldBe(26.23);
            device.LastKnownLocation.Longitude.ShouldBe(92.34);
        }

        [Fact]
        public void Should_Update_Status()
        {
            var device = new IoTDevice(1,7777, "SN100", "Demo", "type");
            device.UpdateStatus(DeviceStatusType.Connected);

            device.Status.ShouldBe(DeviceStatusType.Connected);
        }
    }
}
