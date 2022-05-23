
using System.ComponentModel.DataAnnotations;

namespace FoodTruckJunkie.ApiServer
{
    public class SearchNearestFoodTrucksInput
    {
        //[ValidLatitude(ErrorMessage = "valid latitude between -90 and 90")]
        //[RegularExpression("^(\\+|-)?(?:90(?:(?:\\.0{1,6})?)|(?:[0-9]|[1-8][0-9])(?:(?:\\.[0-9]{1,6})?))$")]
        [Range(-90.00000000, 90.00000000)]
        public string Latitude { get; set; }

       // [ValidLongitude(ErrorMessage = "valid longitude between -180 and 180")]
        [Range(-180.00000000, 180.00000000)]
        //[RegularExpression("^(\\+|-)?(?:180(?:(?:\\.0{1,6})?)|(?:[0-9]|[1-9][0-9]|1[0-7][0-9])(?:(?:\\.[0-9]{1,6})?))$")]
        public string Longitude { get; set; }

        // [Range(0, int.MaxValue)]
        // public int DistantMiles { get; set; }

        // [Range(0, int.MaxValue)]
        // public int NoOfResult { get; set; }
    }
}