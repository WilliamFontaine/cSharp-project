using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.ApiModels
{
    public class Maintenance
    {
        public int Id { get; set; }

        public int VehicleId { get; set; }

        public int MaintenanceMileage { get; set; }

        [Required]
        public string WorkDescription { get; set; }
    }
}
