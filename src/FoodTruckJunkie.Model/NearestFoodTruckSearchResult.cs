using System;
using System.Collections.Generic;

namespace FoodTruckJunkie.Model
{
    public class NearestFoodTruckSearchResult
    {
        public bool HasNearestFoodTruck { get; set; }
        public IEnumerable<NearestFoodTruck> NearestFoodTrucks { get; set; }
    }
}
