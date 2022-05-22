using System;
using System.Collections.Generic;
using FoodTruckJunkie.Model;

namespace FoodTruckJunkie.Service
{
    public interface IFoodTruckPermitService
    {
        public NearestFoodTruckSearchResult SearchNearestFoodTrucks
            (decimal latitude, decimal longtitude, int distantMiles, int noOfResult);
    }
}
