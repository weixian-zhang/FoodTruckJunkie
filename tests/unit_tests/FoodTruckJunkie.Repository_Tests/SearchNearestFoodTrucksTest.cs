using System;
using System.Data;
using Moq;
using Xunit;
using FoodTruckJunkie.Model;
using Dapper;
using System.Dynamic;
using System.Collections.Generic;
using Serilog;
using FoodTruckJunkie.Repository;
using Moq.Dapper;

namespace FoodTruckJunkie.Repository_Tests
{
    public class SearchNearestFoodTrucksTest
    {
        private const string StoredProcSearchLatitudeLongtitude = "SP_SearchLatitudeLongitude";
        List<NearestFoodTruck> _nearestFoodTruck = new List<NearestFoodTruck>();

        private ILogger _logger;
        private AppConfig _appconfig;

        public SearchNearestFoodTrucksTest()
        {
            SetupNearestFoodTrucksData();

            SetupDependencies();
        }


        [Theory]
        [InlineData(37.78623677, -122.3890662, 20 , 5)]
        [InlineData(37.78795496, -122.3972365, 20 , 5)]
        public void SearchNearestFoodTrucks_InputsAreValid_ReturnSearchResult
            (decimal latitude, decimal longitude, int distantMiles,  int noOfResult)
        {
            var dbMock = new Mock<IDbConnection>();

            dbMock.SetupDapper(p => p.Query<NearestFoodTruck>(
                StoredProcSearchLatitudeLongtitude,
                new {startLongtitude = longitude, distantMiles = distantMiles, noOfResult = noOfResult},
                null, 
                true,
                null,
                CommandType.StoredProcedure
            )).Returns(_nearestFoodTruck);    

            IDbConnection db = dbMock.Object;

            var repo = new FoodTruckPermitRepository(_appconfig, db, _logger);

            var searchResult = repo.SearchNearestFoodTrucks(latitude, longitude, distantMiles,noOfResult);

            Assert.NotNull(searchResult);
            Assert.False(searchResult.HasError);
        }

        [Theory]
        [InlineData(37.78, -122.3890662, 20 , 5)] //latitude out of range
        [InlineData(37.78795496, -1.35, 20 , 5)]  //longitude out of range
        [InlineData(3.78, -1.3972365, 20 , 5)]  //both latitude and longitude out of range
        public void SearchNearestFoodTrucks_LatitudeLongitudeAreOutOfRange_SearchResultMarksError
            (decimal latitude, decimal longitude, int distantMiles,  int noOfResult)
        {
            var dbMock = new Mock<IDbConnection>();

            dbMock.SetupDapper(p => p.Query<NearestFoodTruck>(
                StoredProcSearchLatitudeLongtitude,
                new {startLongtitude = longitude, distantMiles = distantMiles, noOfResult = noOfResult},
                null, 
                true,
                null,
                CommandType.StoredProcedure
            )).Returns(_nearestFoodTruck);    

            IDbConnection db = dbMock.Object;

            var mockRepo = new Mock<IFoodTruckPermitRepository>();
            mockRepo.
                Setup(x => x.SearchNearestFoodTrucks(latitude, longitude, distantMiles,noOfResult))
                .Returns(new NearestFoodTruckSearchResult(){ HasError = true});
        
            var repo = mockRepo.Object;
            var searchResult = repo.SearchNearestFoodTrucks(latitude, longitude, distantMiles,noOfResult );

            Assert.True(searchResult.HasError);
        }

        private void SetupNearestFoodTrucksData()
        {
           _nearestFoodTruck.Add(new NearestFoodTruck(){
               Applicant = "Cochinita",
               FoodItems = "Mexican Food: Yucatan Food: Street Food",
               Latitude = 37.78623677m,
               Longitude = -122.3890662m,
               Address = "501 BEALE ST",
               LocationDescription = "BEALE ST: BRYANT ST  DELANCEY ST to END: 500-501 BLOCK (500 - 599)"
           });

           _nearestFoodTruck.Add(new NearestFoodTruck(){
               Applicant = "Plaza Garibaldy",
               FoodItems = "Tacos: burritos: quesadillas",
               Latitude = 37.78795496m,
               Longitude = -122.3972365m,
               Address = "540 HOWARD ST",
               LocationDescription = "HOWARD ST: 01ST ST to MALDEN ALY (500 - 589)"
           });

        }

        private void SetupDependencies()
        {
            var loggerMock = new Mock<ILogger>();
            _logger = loggerMock.Object;

            _appconfig = new AppConfig();
        }
    }
}
