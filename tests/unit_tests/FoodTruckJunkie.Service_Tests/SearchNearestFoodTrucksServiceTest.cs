using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using FoodTruckJunkie.Model;
using FoodTruckJunkie.Repository;
using FoodTruckJunkie.Service;
using Moq;
using Serilog;
using Xunit;

namespace FoodTruckJunkie.Service_Tests
{
    public class SearchNearestFoodTrucksServiceTest
    {
        List<NearestFoodTruck> _nearestFoodTrucks = new List<NearestFoodTruck>();

        private ILogger _logger;
        private AppConfig _appconfig;

        public SearchNearestFoodTrucksServiceTest()
        {
            SetupNearestFoodTrucksData();

            SetupDependencies();
        }


        [Theory]
        [InlineData(37.78623677, -122.3890662, 20 , 5)]
        [InlineData(37.78795496, -122.3972365, 4 , 10)]
        [InlineData(37.78844616, -122.3925795, 5 , 30)]
        [InlineData(37.79215055, -122.394, 11 , 45)]
        public void SearchNearestFoodTrucks_NoOfResultParamBetween_5To50_NoOfResultShouldBeBetween_5To50
            (decimal latitude, decimal longitude, int distantMiles,  int noOfResult)
        {
            SetupNearestFoodTrucksData(45); //set 70 result FoodItems

            var repoMock = new Mock<IFoodTruckPermitRepository>();

            repoMock.Setup(x => x.SearchNearestFoodTrucks(latitude, longitude, distantMiles, It.IsAny<int>()))
                .Returns(new NearestFoodTruckSearchResult(){
                    HasError = false,
                    NearestFoodTrucks = _nearestFoodTrucks,
                    HasNearestFoodTruck = true
                });

            IFoodTruckPermitRepository repo = repoMock.Object;

            IFoodTruckPermitService service = new FoodTruckPermitService(_appconfig, repo, _logger);

            var result = service.SearchNearestFoodTrucks(latitude, longitude, distantMiles, noOfResult);

            Assert.NotNull(result);
            Assert.True(result.NearestFoodTrucks.Count() >= 5);
        }

        [Theory]
        [InlineData(37.78623677, -122.3890662, 20 , -5)]
        [InlineData(37.78795496, -122.3972365, 4 , 0)]
        [InlineData(37.78844616, -122.3925795, 5 , 1)]
        [InlineData(37.79215055, -122.394, 11 , 4)]
        public void SearchNearestFoodTrucks_NoOfResultParamSmallerThan5_NoOfResultShouldBeDefaultedTo_5
            (decimal latitude, decimal longitude, int distantMiles,  int noOfResult)
        {
            SetupNearestFoodTrucksData(5); //set 10 result items

            var repoMock = new Mock<IFoodTruckPermitRepository>();

            //noOfResult has to accept any Interget instead of pass in param for Setup.Returns to return
            //as Setup.Returns expect an exact params match, if not match returns null always
            repoMock.Setup(x => x.SearchNearestFoodTrucks(latitude, longitude, distantMiles, It.IsAny<int>()))
                .Returns( 
                    new NearestFoodTruckSearchResult() {
                    HasError = false,
                    NearestFoodTrucks = _nearestFoodTrucks,
                    HasNearestFoodTruck = true
                });

            IFoodTruckPermitRepository repo = repoMock.Object;

            var service = new FoodTruckPermitService(_appconfig, repo, _logger);

            var result = service.SearchNearestFoodTrucks(latitude, longitude, distantMiles, noOfResult);

            Assert.NotNull(result);
            Assert.True(result.NearestFoodTrucks.Count() == 5);
        }

        [Theory]
        [InlineData(37.78623677, -122.3890662, 20 , 100)]
        [InlineData(37.78795496, -122.3972365, 4 , 51)]
        [InlineData(37.78844616, -122.3925795, 5 , 10000)]
        [InlineData(37.79215055, -122.394, 11 , 80)]
        public void SearchNearestFoodTrucks_NoOfResultParamLargerThan50_NoOfResultShouldBeDefaultedTo_50
            (decimal latitude, decimal longitude, int distantMiles,  int noOfResult)
        {
            SetupNearestFoodTrucksData(50); //set 50 result items

            var repoMock = new Mock<IFoodTruckPermitRepository>();

            repoMock.Setup(x => x.SearchNearestFoodTrucks(latitude, longitude, distantMiles, It.IsAny<int>()))
                .Returns(new NearestFoodTruckSearchResult(){
                    HasError = false,
                    NearestFoodTrucks = _nearestFoodTrucks,
                    HasNearestFoodTruck = true
                });

            IFoodTruckPermitRepository repo = repoMock.Object;

            IFoodTruckPermitService service = new FoodTruckPermitService(_appconfig, repo, _logger);

            var result = service.SearchNearestFoodTrucks(latitude, longitude, distantMiles, noOfResult);

            Assert.NotNull(result);
            Assert.True(result.NearestFoodTrucks.Count() == 50);
        }



        private void SetupNearestFoodTrucksData()
        {
            _nearestFoodTrucks.Clear();

            for(int i = 0; i < 5; i ++) {
                _nearestFoodTrucks.Add(new NearestFoodTruck(){
                    Applicant = "Cochinita",
                    FoodItems = "Mexican Food: Yucatan Food: Street Food",
                    Latitude = 37.78623677m,
                    Longitude = -122.3890662m,
                    Address = "501 BEALE ST",
                    LocationDescription = "BEALE ST: BRYANT ST  DELANCEY ST to END: 500-501 BLOCK (500 - 599)"
             });
            }
        }

        private void SetupNearestFoodTrucksData(int noOfResult)
        {
            _nearestFoodTrucks.Clear();

            for(int i = 0; i < noOfResult; i ++) {
                _nearestFoodTrucks.Add(new NearestFoodTruck(){
                    Applicant = "Cochinita",
                    FoodItems = "Mexican Food: Yucatan Food: Street Food",
                    Latitude = 37.78623677m,
                    Longitude = -122.3890662m,
                    Address = "501 BEALE ST",
                    LocationDescription = "BEALE ST: BRYANT ST  DELANCEY ST to END: 500-501 BLOCK (500 - 599)"
             });
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
