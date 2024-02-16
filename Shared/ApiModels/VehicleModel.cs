using System.ComponentModel.DataAnnotations;

namespace Shared.ApiModels
{
    public abstract class VehicleModel
    {
        public int Id { get; set; }

        public VehicleBrand Brand { get; set; }

        [MinLength(1)]
        public string Model { get; set; }

        public int MaintenanceRate { get; set; }
    }
}
