using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.ApiModels;

namespace cSharp_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleModelController(DbContext context) : ControllerBase
    {

        private readonly DbContext _context = context;

        private DbSet<VehicleModel> VehicleModelRepository => _context.Set<VehicleModel>();

        [HttpGet]
        public IActionResult Get()
        {
            var vehicleModels = VehicleModelRepository.ToList();
            if (vehicleModels.Count == 0)
            {
                return NotFound();
            }
            return Ok(vehicleModels);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var vehicleModel = VehicleModelRepository.Find(id);
            if (vehicleModel == null)
            {
                return NotFound();
            }
            return Ok(vehicleModel);
        }

        [HttpPost]
        public IActionResult Post([FromBody] VehicleModel vehicleModel)
        {
            VehicleModelRepository.Add(vehicleModel);
            _context.SaveChanges();
            return Created($"/api/vehicleModel/{vehicleModel.Id}", vehicleModel);
        }


        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] VehicleModel vehicleModel)
        {
            var existingVehicleModel = VehicleModelRepository.Find(id);
            if (existingVehicleModel == null)
            {
                return NotFound();
            }
            existingVehicleModel.Brand = vehicleModel.Brand;
            existingVehicleModel.Model = vehicleModel.Model;
            existingVehicleModel.MaintenanceRate = vehicleModel.MaintenanceRate;
            _context.SaveChanges();
            return Ok(existingVehicleModel);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var vehicleModel = VehicleModelRepository.Find(id);
            if (vehicleModel == null)
            {
                return NotFound();
            }
            foreach (var vehicle in vehicleModel.Vehicles)
            {
                _context.Remove(vehicle);
            }
            _context.SaveChanges();
            VehicleModelRepository.Remove(vehicleModel);
            _context.SaveChanges();
            return Ok();
        }
    }
}
