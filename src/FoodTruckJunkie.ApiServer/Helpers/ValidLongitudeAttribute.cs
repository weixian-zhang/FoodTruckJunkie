using System.ComponentModel.DataAnnotations;

namespace FoodTruckJunkie.ApiServer
{
    public class ValidLongitudeAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            double result;
            if(!double.TryParse(value.ToString(), out result))
                return false;
        

            if (result < -180 || result > 180)
                return true;

            return false;
        }
    }
}