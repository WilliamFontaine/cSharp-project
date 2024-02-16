using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Shared.ApiModels
{
    public class VehicleModel
    {
        public int Id { get; set; }

        public VehicleBrand Brand { get; set; }

        [MinLength(1)]
        public string Model { get; set; }

        public int MaintenanceRate { get; set; }

        [JsonIgnore]
        public IList<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
    }
}
