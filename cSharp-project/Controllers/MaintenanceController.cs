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

        [HttpGet]
        public IActionResult Get()
        {
            var maintenances = MaintenanceRepository.ToList();
            if (maintenances.Count == 0)
            {
                return NotFound();
            }
            return Ok(maintenances);
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
            MaintenanceRepository.Add(maintenance);
            _context.SaveChanges();
            return Created($"/api/maintenance/{maintenance.Id}", maintenance);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Maintenance maintenance)
        {
            var existingMaintenance = MaintenanceRepository.Find(id);
            if (existingMaintenance == null)
            {
                return NotFound();
            }
            existingMaintenance.MaintenanceMileage = maintenance.MaintenanceMileage;
            existingMaintenance.WorkDescription = maintenance.WorkDescription;
            _context.SaveChanges();
            return Ok(existingMaintenance);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var maintenance = MaintenanceRepository.Find(id);
            if (maintenance == null)
            {
                return NotFound();
            }
            MaintenanceRepository.Remove(maintenance);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
