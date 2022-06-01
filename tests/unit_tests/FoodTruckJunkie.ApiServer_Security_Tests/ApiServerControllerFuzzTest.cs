using System;
using FoodTruckJunkie.ApiServer.Controllers;
using FoodTruckJunkie.Service;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Serilog;
using Serilog.Core;
using Xunit;

//https://andrewlock.net/creating-a-custom-xunit-theory-test-dataattribute-to-load-data-from-json-files/

namespace FoodTruckJunkie.ApiServer_Security_Tests
{
    public class ApiServerControllerFuzzTest
    {
        [Theory]
        [SixDigitNumbersFuzzDataAttribute()]
        public void FoodTruckPermitController_FuzzTests(decimal latitude, decimal logitude, int distantMiles, int noOfResults)
        {
            ILogger logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();

            var mockService = new Mock<IFoodTruckPermitService>();
            mockService.Setup(x => x.SearchNearestFoodTrucks(It.IsAny<decimal>(), It.IsAny<decimal>(), It.IsAny<int>(), It.IsAny<int>()));
            
            var concreteMockService = mockService.Object;
                 
            var controller = new FoodTruckPermitController(concreteMockService, logger);

            IActionResult result =  controller.SearchNearestFoodTrucks(latitude, logitude, distantMiles, noOfResults);

            var okResult = result as OkObjectResult;

            Assert.True(okResult.StatusCode == 200);
            
        }
    }
}
