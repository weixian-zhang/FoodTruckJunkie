using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using FoodTruckJunkie.Model;
using Serilog;

namespace FoodTruckJunkie.Repository
{
    public class FoodTruckPermitRepository : IFoodTruckPermitRepository
    {
        private AppConfig _appconfig;
        private ILogger _logger;
        private IDbConnection _db;
        private const string StoredProcSearchLatitudeLongtitude = "SP_SearchLatitudeLongitude";

        public FoodTruckPermitRepository(AppConfig appconfig, IDbConnection db, ILogger logger) {
            _logger = logger;
            _appconfig = appconfig;
            _db = db;
        }

        public NearestFoodTruckSearchResult SearchNearestFoodTrucks
            (decimal latitude, decimal longtitude, int distantMiles, int noOfResult)
        {
            try
            {
                var result = _db.Query<NearestFoodTruck>(StoredProcSearchLatitudeLongtitude,
                new {startLatitude = latitude, startLongtitude = longtitude, distantMiles = distantMiles, noOfResult = noOfResult}, 
                commandType: CommandType.StoredProcedure);

                bool hasNearestFoodTrucks = result.Count() > 0 ? true : false;

                return new NearestFoodTruckSearchResult()
                {
                    HasNearestFoodTruck = hasNearestFoodTrucks,
                    NearestFoodTrucks = result
                };
            }
            catch(Exception ex)
            {
                _logger.Error(ex.ToString());
                
                return new NearestFoodTruckSearchResult()
                {
                    HasNearestFoodTruck = false,
                    HasError = true,
                    NearestFoodTrucks = new List<NearestFoodTruck>()
                };
            }
            
        }
    }
}