﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
            string validationMessage = "";
            if(!IsLatitudeLongitudeValid(latitude, longitude, out validationMessage)) {
                return BadRequest(validationMessage);
            }
            
           var result =  _ftService.SearchNearestFoodTrucks(latitude, longitude, distantMiles, noOfResult);
           return Ok(result);
        }

        private bool IsLatitudeLongitudeValid(decimal latitude, decimal longitude, out string validationMessages)
        {
            var inputs = new SearchNearestFoodTrucksInput() {
                Latitude = latitude.ToString(),
                Longitude = longitude.ToString()
            };

            var valContext = new ValidationContext(inputs, null, null);

            var valResults = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(inputs, valContext, valResults, true);

            validationMessages = string.Join("\r\n", valResults);

            if(!isValid)
                _logger.Information($"Invalid paramters supplied: {validationMessages}");

            return isValid;
            
        }
    }
}
