using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodTruckJunkie.Model;
using FoodTruckJunkie.Service;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace FoodTruckJunkie.ApiServer.Controllers
{
    [ApiController]
    [Route("api/")]
    public class FoodTruckPermitController : ControllerBase
    {
        private IFoodTruckPermitService _ftService;
        private readonly ILogger _logger;

        public FoodTruckPermitController(IFoodTruckPermitService ftService, ILogger  logger)
        {
            _ftService = ftService;
            _logger = logger;
        }

public decimal Latitude { get; set; }

        [HttpGet("searchfoodtrucks")]
        public IActionResult SearchNearestFoodTrucks
            ([FromQuery] decimal latitude, decimal longitude, int distantMiles, int noOfResult )
        {
           var result =  _ftService.SearchNearestFoodTrucks(latitude, longitude, distantMiles, noOfResult);
           return Ok(result);
        }
    }
}
