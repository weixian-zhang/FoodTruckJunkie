
using System.ComponentModel.DataAnnotations;

namespace FoodTruckJunkie.ApiServer
{
    public class SearchNearestFoodTrucksInput
    {
        [RegularExpression(@"^\d+(\.\d{1,2})?$")]
        [Range(0, 9999999999.99999999)]
        public decimal Latitude { get; set; }

        [RegularExpression(@"^\d+(\.\d{1,2})?$")]
        [Range(0, 99999999999.99999999)]
        public decimal Longitude { get; set; }

        [Range(0, int.MaxValue)]
        public int DistantMiles { get; set; }

        [Range(0, int.MaxValue)]
        public int NoOfResult { get; set; }
    }
}