using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace cSharp_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {

        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }

    }
}
