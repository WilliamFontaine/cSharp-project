using System.ComponentModel.DataAnnotations;

namespace Shared.ApiModels
{
    public class Maintenance
    {
        public int Id { get; set; }

        public int MaintenanceMileage { get; set; }

        [Required]
        public string WorkDescription { get; set; }

        public int VehicleId { get; set; }

    }
}
