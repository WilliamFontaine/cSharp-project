using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.ApiModels;

namespace cSharp_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController(DbContext context) : ControllerBase
    {
        private readonly DbContext _context = context;

        private DbSet<Vehicle> VehicleRepository => _context.Set<Vehicle>();

        private DbSet<VehicleModel> VehicleModelRepository => _context.Set<VehicleModel>();

        [HttpGet]
        public IActionResult Get()
        {
            var vehicles = VehicleRepository.Include(v => v.Maintenances).Include(v => v.VehicleModel).ToList();
            if (vehicles.Count == 0)
            {
                return NoContent();
            }

            return Ok(vehicles);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var vehicle = VehicleRepository.Find(id);
            if (vehicle == null)
            {
                return NotFound();
            }

            return Ok(vehicle);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Vehicle vehicle)
        {
            VehicleRepository.Add(vehicle);
            var vehicleModel = VehicleModelRepository.Find(vehicle.VehicleModelId);
            if (vehicleModel == null)
            {
                return NotFound("Related vehicle model not found");
            }

            _context.SaveChanges();
            return Created($"/api/vehicle/{vehicle.Id}", vehicle);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Vehicle vehicle)
        {
            var existingVehicle = VehicleRepository.Find(id);
            if (existingVehicle == null)
            {
                return NotFound();
            }

            existingVehicle.VehicleModelId = vehicle.VehicleModelId;
            existingVehicle.LicensePlate = vehicle.LicensePlate;
            existingVehicle.Year = vehicle.Year;
            existingVehicle.Mileage = vehicle.Mileage;
            existingVehicle.Energy = vehicle.Energy;
            var vehicleModel = VehicleModelRepository.Find(vehicle.VehicleModelId);
            if (vehicleModel == null)
            {
                return NotFound("Related vehicle model not found");
            }

            _context.SaveChanges();
            return Ok(existingVehicle);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var vehicle = VehicleRepository.Find(id);
            if (vehicle == null)
            {
                return NotFound();
            }

            VehicleRepository.Remove(vehicle);
            _context.SaveChanges();
            return NoContent();
        }
    }
}