using System;
using System.Collections.Generic;
using FoodTruckJunkie.Model;

namespace FoodTruckJunkie.Service
{
    public interface IFoodTruckPermitService
    {
        public IEnumerable<LatitudeLongitudeSearchResult> SearchLatitudeLongtitude
            (decimal lat, decimal longtitude, int distantMiles, int noOfResult);
    }
}
