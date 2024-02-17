using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Shared.ApiModels
{
    public class Vehicle
    {
        public int Id { get; set; }

        [MinLength(7)]
        [MaxLength(9)]
        public string LicensePlate { get; set; }

        [Range(1900, 2100)]
        public int Year { get; set; }

        [Range(0, int.MaxValue)]
        public int Mileage { get; set; }

        public VehicleEnergy Energy { get; set; }

        public int VehicleModelId { get; set; }

        public virtual IList<Maintenance>? Maintenances { get; set; }

        public virtual VehicleModel? VehicleModel { get; set; }
    }
}