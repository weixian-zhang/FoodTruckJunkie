using System;
using System.Collections.Generic;
using FoodTruckJunkie.Model;

namespace FoodTruckJunkie.Repository
{
    public interface IFoodTruckPermitRepository
    {
        public IEnumerable<LatitudeLongitudeSearchResult> SearchLatitudeLongtitude
            (decimal lat, decimal longtitude, int distantMiles, int noOfResult);
    }
}
