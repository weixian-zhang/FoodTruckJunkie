﻿using System;

namespace FoodTruckJunkie.Model
{
    public class NearestFoodTruck
    {
        //public int id { get; set; }
        public string Applicant { get; set; }
        public string FoodItems { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude  { get; set; }
        public string Address { get; set; }
        public string LocationDescription { get; set; }
    }
}
