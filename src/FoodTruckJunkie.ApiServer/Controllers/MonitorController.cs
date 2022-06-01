using Microsoft.AspNetCore.Mvc;

namespace FoodTruckJunkie.ApiServer.Controllers
{
    [ApiController]
    [Route("")]
    [ApiVersion("1.0")]
    public class MonitorController : ControllerBase
    {
        [HttpGet("health/{version:apiVersion}")]
        [MapToApiVersion("1.0")]
        public JsonResult GetHealth()
        {
            dynamic result = new {
                status= "alive"
            };

            return new JsonResult(result);
        }
    }
}
