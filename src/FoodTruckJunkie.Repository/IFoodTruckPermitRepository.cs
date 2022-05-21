using System;
using System.Collections.Generic;
using FoodTruckJunkie.Model;

namespace FoodTruckJunkie.Repository
{
    public interface IFoodTruckPermitRepository
    {
        public NearestFoodTruckSearchResult SearchNearestFoodTrucks
            (decimal lat, decimal longtitude, int distantMiles, int noOfResult);
    }
}
