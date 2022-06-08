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
        private const int NoOfResultLowerLimit = 5;
        private const int NoOfResultUpperLimit = 50;
        private const int ProximityMilesLowwerLimit = 1;
        private const int ProximityMilesUpperLimit = 30;

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
           distantMiles = LimitProximity(distantMiles);
           noOfResult = LimitNoOfResult(noOfResult);
            
           var result = _permitRepo.SearchNearestFoodTrucks(lat, longitude, distantMiles, noOfResult);
           return result;           
        }

        private int LimitNoOfResult(int noOfResult) {
            if(noOfResult < NoOfResultLowerLimit)
                noOfResult = NoOfResultLowerLimit;
            else if (noOfResult > NoOfResultUpperLimit)
                noOfResult = NoOfResultUpperLimit;

            return noOfResult;
        }

        private int LimitProximity(int distantMiles) {
            if(distantMiles < ProximityMilesLowwerLimit)
                distantMiles = ProximityMilesLowwerLimit;
            else if (distantMiles > ProximityMilesUpperLimit)
                distantMiles = ProximityMilesUpperLimit;

            return distantMiles;
        }
    }
}
