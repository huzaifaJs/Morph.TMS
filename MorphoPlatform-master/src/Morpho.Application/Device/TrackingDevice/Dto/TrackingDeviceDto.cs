using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Morpho.Domain.Entities.Devices;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Morpho.Device.TrackingDeviceDto
{
    [AutoMapTo(typeof(TrackingDevices))]
    public class CreateDeviceDto
    {
        private string _devicetypename;
        private string _deviceuniqueno;
        private string _remark;
        private string _devicename;
        private string _manufacturer;
        private string _serialnumber;
        private string _imeinumber;

        [MaxLength(250)]
        public string device_type_name
        {
            get => _devicetypename;
            set => _devicetypename = value?.Trim();
        }

        [MaxLength(600)]
        public string remark
        {
            get => _remark;
            set => _remark = value?.Trim();
        }
        [MaxLength(250)]
        public string device_unique_no
        {
            get => _deviceuniqueno;
            set => _deviceuniqueno = value?.Trim();
        }
        [MaxLength(250)]
        public string device_name
        {
            get => _devicename;
            set => _devicename = value?.Trim();
        }
        [MaxLength(250)]
        public string manufacturer
        {
            get => _manufacturer;
            set => _manufacturer = value?.Trim();
        }
        [MaxLength(250)]
        public string serial_number
        {
            get => _serialnumber;
            set => _serialnumber = value?.Trim();
        }
        [MaxLength(250)]
        public string imei_number
        {
            get => _imeinumber;
            set => _imeinumber = value?.Trim();
        }
        public decimal? min_value { get; set; }
        public decimal? max_value { get; set; }
    }

    [AutoMapTo(typeof(TrackingDevices))]
    public class UpdateDeviceDto
    {
        [Required]
        public long Id { get; set; }

        private string _devicetypename;
        private string _deviceuniqueno;
        private string _remark;
        private string _devicename;
        private string _manufacturer;
        private string _serialnumber;
        private string _imeinumber;

        [MaxLength(250)]
        public string device_type_name
        {
            get => _devicetypename;
            set => _devicetypename = value?.Trim();
        }

        [MaxLength(600)]
        public string remark
        {
            get => _remark;
            set => _remark = value?.Trim();
        }
        [MaxLength(250)]
        public string device_unique_no
        {
            get => _deviceuniqueno;
            set => _deviceuniqueno = value?.Trim();
        }
        [MaxLength(250)]
        public string device_name
        {
            get => _devicename;
            set => _devicename = value?.Trim();
        }
        [MaxLength(250)]
        public string manufacturer
        {
            get => _manufacturer;
            set => _manufacturer = value?.Trim();
        }
        [MaxLength(250)]
        public string serial_number
        {
            get => _serialnumber;
            set => _serialnumber = value?.Trim();
        }
        [MaxLength(250)]
        public string imei_number
        {
            get => _imeinumber;
            set => _imeinumber = value?.Trim();
        }
        public decimal? min_value { get; set; }
        public decimal? max_value { get; set; }
    }
    [AutoMapTo(typeof(TrackingDevices))]
    public class UpdateStatusDeviceDto
    {
        [Required]
        public long Id { get; set; }
    }

    [AutoMapFrom(typeof(TrackingDevices))]
    public class DeviceDto : EntityDto<long>
    {
        public string device_type_name { get; set; }
        public string device_name { get; set; }
        public string manufacturer { get; set; }
        public string device_unique_no { get; set; }
        public string model_no { get; set; }
        public string remark { get; set; }
        public string serial_number { get; set; }
        public string imei_number { get; set; }
        public decimal min_value { get; set; }
        public decimal? max_value { get; set; }
        public decimal? isblock { get; set; }
        public DateTime Created_At { get; set; }
        public long? Created_By { get; set; }
        public DateTime? Updated_At { get; set; }
        public long? Updated_By { get; set; }
    }

    public class DevicePagedRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
    }
}
