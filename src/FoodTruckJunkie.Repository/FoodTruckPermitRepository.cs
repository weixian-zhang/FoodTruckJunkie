using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using FoodTruckJunkie.Model;
using Serilog;

namespace FoodTruckJunkie.Repository
{
    public class FoodTruckPermitRepository : BaseRepository, IFoodTruckPermitRepository
    {
        private AppConfig _appconfig;
        private ILogger _logger;
        private const string StoredProcSearchLatitudeLongtitude = "SP_SearchLatitudeLongitude";

        public FoodTruckPermitRepository(AppConfig appconfig, ILogger logger) : base(appconfig.MySQLConnectionString)
        {
            _logger = logger;
            _appconfig = appconfig;
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