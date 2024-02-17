using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.ApiModels;

namespace cSharp_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaintenanceController(DbContext context) : ControllerBase
    {
        private readonly DbContext _context = context;

        private DbSet<Maintenance> MaintenanceRepository => _context.Set<Maintenance>();

        private DbSet<Vehicle> VehicleRepository => _context.Set<Vehicle>();

        [HttpGet]
        public IActionResult Get()
        {
            var maintenances = MaintenanceRepository.ToList();
            if (maintenances.Count == 0)
            {
                return NoContent();
            }

            return Ok(maintenances);
        }

        [HttpGet("late-maintenance")]
        public IActionResult GetLateMaintenance()
        {
            var maintenances = MaintenanceRepository
                .Include(m => m.Vehicle)
                .Include(m => m.Vehicle!.VehicleModel)
                .ToList();
            if (maintenances.Count == 0)
            {
                return NoContent();
            }

            var lateMaintenances = maintenances
                .GroupBy(m => m.VehicleId)
                .Select(g => g.OrderByDescending(m => m.MaintenanceMileage).First())
                .Where(m => m.Vehicle?.Mileage > (m.MaintenanceMileage + m.Vehicle?.VehicleModel?.MaintenanceRate))
                .ToList();

            if (lateMaintenances.Count == 0)
            {
                return NoContent();
            }

            return Ok(lateMaintenances);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var maintenance = MaintenanceRepository.Find(id);
            if (maintenance == null)
            {
                return NotFound();
            }

            return Ok(maintenance);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Maintenance maintenance)
        {
            var vehicle = VehicleRepository.Find(maintenance.VehicleId);
            if (vehicle == null)
            {
                return NotFound("Related vehicle not found");
            }

            maintenance.MaintenanceMileage = vehicle.Mileage;
            MaintenanceRepository.Add(maintenance);
            _context.SaveChanges();
            return Created($"/api/maintenance/{maintenance.Id}", maintenance);
        }
    }
}