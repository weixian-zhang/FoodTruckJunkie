using System;
using System.Collections.Generic;
using FoodTruckJunkie.ApiServer.Controllers;
using FoodTruckJunkie.Model;
using FoodTruckJunkie.Service;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Serilog;
using Xunit;

namespace FoodTruckJunkie.ApiServer_Tests
{
    public class SearchNearestFoodTrucksControllerTest
    {
        List<NearestFoodTruck> _nearestFoodTrucks = new List<NearestFoodTruck>();

        private ILogger _logger;
        private AppConfig _appconfig;

        public SearchNearestFoodTrucksControllerTest()
        {
            SetupNearestFoodTrucksData();

            SetupDependencies();
        }

        [Theory]
        [InlineData(37.78623677, -122.3890662, 20 , 100)]
        [InlineData(37.78795496, -122.3972365, 4 , 51)]
        [InlineData(37.78844616, -122.3925795, 5 , 10000)]
        [InlineData(37.79215055, -122.394, 11 , 80)]
        public void SearchNearestFoodTrucks_WithValidLatitudeLongitude_ReturnHttpOK
            (decimal latitude, decimal longitude, int distantMiles,  int noOfResult)
        {
            var mockService = new Mock<IFoodTruckPermitService>();
            mockService.Setup
                (x => x.SearchNearestFoodTrucks(It.IsAny<decimal>(), It.IsAny<decimal>(), distantMiles, noOfResult))
                .Returns(new NearestFoodTruckSearchResult(){
                    HasError = false,
                    NearestFoodTrucks = _nearestFoodTrucks,
                    HasNearestFoodTruck = true
                });

            IFoodTruckPermitService service = mockService.Object;

            var controller = new FoodTruckPermitController(service, _logger);

            var result = controller.SearchNearestFoodTrucks(latitude, longitude, distantMiles,noOfResult);

            var okResult = result as OkObjectResult;

            Assert.Equal(okResult.StatusCode, 200);
        }

        [Theory]
        [InlineData(37.78623677, -222.3890662, 20 , 100)]
        [InlineData(-137.78795496, -192.3972365, 4 , 51)]
        [InlineData(91.78844616, -122.3925795, 5 , 10000)]
        [InlineData(-337.79215055, -122.394, 11 , 80)]
        public void SearchNearestFoodTrucks_WithValidLatitudeLongitude_ReturnHttpForbid
            (decimal latitude, decimal longitude, int distantMiles,  int noOfResult)
        {
            var mockService = new Mock<IFoodTruckPermitService>();
            mockService.Setup
                (x => x.SearchNearestFoodTrucks(It.IsAny<decimal>(), It.IsAny<decimal>(), distantMiles, noOfResult))
                .Returns(new NearestFoodTruckSearchResult(){
                    HasError = false,
                    NearestFoodTrucks = _nearestFoodTrucks,
                    HasNearestFoodTruck = true
                });

            IFoodTruckPermitService service = mockService.Object;

            var controller = new FoodTruckPermitController(service, _logger);

            var result = controller.SearchNearestFoodTrucks(latitude, longitude, distantMiles,noOfResult);

            Assert.IsType<ForbidResult>(result);
        }

        private void SetupNearestFoodTrucksData()
        {
            _nearestFoodTrucks.Clear();

            int x = 0;
            
            while(x <= 10) {
                _nearestFoodTrucks.Add(new NearestFoodTruck(){
                    Applicant = "Cochinita",
                    FoodItems = "Mexican Food: Yucatan Food: Street Food",
                    Latitude = 37.78623677m,
                    Longitude = -122.3890662m,
                    Address = "501 BEALE ST",
                    LocationDescription = "BEALE ST: BRYANT ST  DELANCEY ST to END: 500-501 BLOCK (500 - 599)"
                });

                x++;
            }
        }

        private void SetupDependencies()
        {
            var loggerMock = new Mock<ILogger>();
            _logger = loggerMock.Object;

            _appconfig = new AppConfig();
        }
    }
}
