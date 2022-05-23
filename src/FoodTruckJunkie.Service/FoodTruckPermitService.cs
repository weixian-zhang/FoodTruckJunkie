using System;
using System.Collections.Generic;
using System.Linq;
using FoodTruckJunkie.Model;
using FoodTruckJunkie.Repository;
using Serilog;

namespace FoodTruckJunkie.Service
{
    public class FoodTruckPermitService : IFoodTruckPermitService
    {
        private IFoodTruckPermitRepository _permitRepo;
        private AppConfig _appconfig;
        private ILogger _logger;

        public FoodTruckPermitService(AppConfig appconfig, IFoodTruckPermitRepository permitRepo, ILogger logger)
        {
            _appconfig = appconfig;
            _permitRepo = permitRepo;    
            _logger = logger;
        }
        
        public NearestFoodTruckSearchResult SearchNearestFoodTrucks
            (decimal lat, decimal longitude, int distantMiles, int noOfResult)
        {
            if(noOfResult < 5)
                noOfResult = 5;
            else if (noOfResult > 50)
                noOfResult = 50;

           var result = _permitRepo.SearchNearestFoodTrucks(lat, longitude, distantMiles, noOfResult);
           return result;           
        }
    }
}
