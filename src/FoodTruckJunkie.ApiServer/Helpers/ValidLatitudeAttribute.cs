using System;
using System.ComponentModel.DataAnnotations;

namespace FoodTruckJunkie.ApiServer
{
    public class ValidLatitudeAttribute : ValidationAttribute
    {
        public  override bool  IsValid(object value)
        {
            double result;
            if(!double.TryParse(value.ToString(), out result))
                return false;
        

            if (result < -90.00000000 || result > 90.00000000)
                return true;

            return false;
            
        }
    }
}