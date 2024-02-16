using System.ComponentModel.DataAnnotations;

namespace Shared.ApiModels
{
    public class Vehicle
    {
        public int Id { get; set; }

        public int VehicleModelId { get; set; }

        [MinLength(7)]
        [MaxLength(9)]
        public string LicensePlate { get; set; }

        public DateTime Year { get; set; }

        [Range(0, int.MaxValue)]
        public int Mileage { get; set; }

        public VehicleEnergy Energy { get; set; }
    }
}
