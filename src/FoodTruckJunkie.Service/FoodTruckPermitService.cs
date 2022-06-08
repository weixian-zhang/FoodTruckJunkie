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
        private const int DistantMilesLowerLimit = 1;
        private const int DistantMilesUpperLimit = 30;

        private IFoodTruckPermitRepository _permitRepo;
        private ILogger _logger;

        public FoodTruckPermitService(IFoodTruckPermitRepository permitRepo, ILogger logger)
        {
            _permitRepo = permitRepo;    
            _logger = logger;
        }
        
        public NearestFoodTruckSearchResult SearchNearestFoodTrucks
            (decimal lat, decimal longitude, int distantMiles, int noOfResult)
        {
           distantMiles = LimitDistantMiles(distantMiles);
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

        private int LimitDistantMiles(int distantMiles) {
            if(distantMiles < DistantMilesLowerLimit)
                distantMiles = DistantMilesLowerLimit;
            else if (distantMiles > DistantMilesUpperLimit)
                distantMiles = DistantMilesUpperLimit;

            return distantMiles;
        }
    }
}
