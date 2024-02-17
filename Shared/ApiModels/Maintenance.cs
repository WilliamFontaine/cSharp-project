using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Shared.ApiModels
{
    public class Maintenance
    {
        public int Id { get; set; }

        [Range(0, int.MaxValue)] public int? MaintenanceMileage { get; set; }

        [Required] public string? WorkDescription { get; set; }

        public int VehicleId { get; set; }

        public virtual Vehicle? Vehicle { get; set; }
    }
}