using System;
using System.Collections.Generic;
using FoodTruckJunkie.Model;
using FoodTruckJunkie.Repository;

namespace FoodTruckJunkie.Service
{
    public class FoodTruckPermitService : IFoodTruckPermitService
    {
        public FoodTruckPermitService(IFoodTruckPermitRepository permitRepo)
        {

        }
        
        public IEnumerable<LatitudeLongitudeSearchResult> SearchLatitudeLongtitude
            (decimal lat, decimal longtitude, int distantMiles, int noOfResult)
        {
            throw new NotImplementedException();
        }
    }
}
