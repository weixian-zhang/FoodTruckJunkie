using System;
using System.Collections.Generic;
using FoodTruckJunkie.Model;

namespace FoodTruckJunkie.Repository
{
    public interface IPermitRepository
    {
        public IEnumerable<LatitudeLongTitudeSearchResult> SearchLatitudeLongtitude(decimal lat, decimal longtitude, int distantMiles);
    }
}
