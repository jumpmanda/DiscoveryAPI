using Microsoft.AspNetCore.Mvc;

namespace DiscoveryApi.Controllers
{
    [Route("api")]
    public class HealthController: Controller
    {     
        [HttpGet("health")]
        public IActionResult IsHealthy()
        {
            return Ok("Healthy!"); 
        }        
    }
}
