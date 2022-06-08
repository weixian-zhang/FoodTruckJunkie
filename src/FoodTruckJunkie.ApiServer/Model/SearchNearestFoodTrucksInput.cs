
using System.ComponentModel.DataAnnotations;

namespace FoodTruckJunkie.ApiServer
{
    public class SearchNearestFoodTrucksInput
    {
        [Range(-90.00000000, 90.00000000)]
        public string Latitude { get; set; }

        [Range(-180.00000000, 180.00000000)]
        public string Longitude { get; set; }
    }
}