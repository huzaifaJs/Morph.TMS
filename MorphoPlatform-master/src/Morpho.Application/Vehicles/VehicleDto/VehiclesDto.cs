using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Morpho.Domain.Entities;
using Morpho.Dto;
using Morpho.VehicleType.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Morpho.Vehicles.VehicleDto
{
    [AutoMapTo(typeof(Morpho.Domain.Entities.Vehicles.Vehicles))]
    public class CreateVehicleDto
    {
        private string _vehiclenumber;
        private string _vehicleunqiueid;
        private string _remark;
        private string _vehiclename;
        private string _manufacturer;
        private string _modelname;
        private string _chassisnumber;
        private string _enginenumber;
        private string  _manufacturingyear;
        public string vehicle_types_id { get; set; }
        public string fuel_types_id { get; set; }
        [MaxLength(250)]
        public string vehicle_number
        {
            get => _vehiclenumber;
            set => _vehiclenumber = value?.Trim();
        }

        [MaxLength(600)]
        public string remark
        {
            get => _remark;
            set => _remark = value?.Trim();
        }
        [MaxLength(250)]
        public string vehicle_unqiue_id
        {
            get => _vehicleunqiueid;
            set => _vehicleunqiueid = value?.Trim();
        }
        [MaxLength(250)]
        public string vehicle_name
        {
            get => _vehiclename;
            set => _vehiclename = value?.Trim();
        }
        [MaxLength(250)]
        public string manufacturer
        {
            get => _manufacturer;
            set => _manufacturer = value?.Trim();
        }
        [MaxLength(250)]
        public string model_name
        {
            get => _modelname;
            set => _modelname = value?.Trim();
        }
        [MaxLength(250)]
        public string chassis_number
        {
            get => _chassisnumber;
            set => _chassisnumber = value?.Trim();
        }
        [MaxLength(250)]
        public string engine_number
        {
            get => _enginenumber;
            set => _enginenumber = value?.Trim();
        }
        [MaxLength(250)]
        public string manufacturing_year
        {
            get => _manufacturingyear;
            set => _manufacturingyear = value?.Trim();
        }
    }

    [AutoMapTo(typeof(Morpho.Domain.Entities.Vehicles.Vehicles))]
    public class UpdateVehicleDto
    {
        [Required]
        public long Id { get; set; }

        private string _vehiclenumber;
        private string _vehicleunqiueid;
        private string _remark;
        private string _vehiclename;
        private string _manufacturer;
        private string _modelname;
        private string _chassisnumber;
        private string _enginenumber;
        private string _manufacturingyear;
        public string vehicle_types_id { get; set; }
        public string fuel_types_id { get; set; }
        [MaxLength(250)]
        public string vehicle_number
        {
            get => _vehiclenumber;
            set => _vehiclenumber = value?.Trim();
        }

        [MaxLength(600)]
        public string remark
        {
            get => _remark;
            set => _remark = value?.Trim();
        }
        [MaxLength(250)]
        public string vehicle_unqiue_id
        {
            get => _vehicleunqiueid;
            set => _vehicleunqiueid = value?.Trim();
        }
        [MaxLength(250)]
        public string vehicle_name
        {
            get => _vehiclename;
            set => _vehiclename = value?.Trim();
        }
        [MaxLength(250)]
        public string manufacturer
        {
            get => _manufacturer;
            set => _manufacturer = value?.Trim();
        }
        [MaxLength(250)]
        public string model_name
        {
            get => _modelname;
            set => _modelname = value?.Trim();
        }
        [MaxLength(250)]
        public string chassis_number
        {
            get => _chassisnumber;
            set => _chassisnumber = value?.Trim();
        }
        [MaxLength(250)]
        public string engine_number
        {
            get => _enginenumber;
            set => _enginenumber = value?.Trim();
        }
        [MaxLength(250)]
        public string manufacturing_year
        {
            get => _manufacturingyear;
            set => _manufacturingyear = value?.Trim();
        }
    }
    [AutoMapTo(typeof(Morpho.Domain.Entities.Vehicles.Vehicles))]
    public class UpdateStatusVehicleDto
    {
        [Required]
        public long Id { get; set; }
    }

    [AutoMapFrom(typeof(Morpho.Domain.Entities.Vehicles.Vehicles))]
    public class VehicleDto : EntityDto<long>
    {
        // IDs
        public long vehicle_types_id { get; set; }
        public long fuel_types_id { get; set; }
        public string vehicle_unqiue_id { get; set; }
        public string vehicle_number { get; set; }
        public string vehicle_name { get; set; }
        public string model_name { get; set; }
        public string manufacturer { get; set; }
        public string model { get; set; }
        public int manufacturing_year { get; set; }
        public string chassis_number { get; set; }
        public string engine_number { get; set; }
        public bool isblock { get; set; }
        public string remark { get; set; }
        public DateTime Created_At { get; set; }
        public long? Created_By { get; set; }
        public DateTime? Updated_At { get; set; }
        public long? Updated_By { get; set; }
        public VehicleTypeDto VehicleType { get; set; }
        public FuelTypeDto FuelType { get; set; }
        public string vehicle_type => VehicleType?.vehicle_type_name;
        public string fuel_type => FuelType?.fuel_type_name;
    }


    public class VehiclePagedRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
    }
}
