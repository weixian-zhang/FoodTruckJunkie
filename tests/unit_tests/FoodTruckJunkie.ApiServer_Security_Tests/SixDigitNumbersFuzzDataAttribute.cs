using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Xunit;
using Xunit.Sdk;

//https://andrewlock.net/creating-a-custom-xunit-theory-test-dataattribute-to-load-data-from-json-files/

namespace FoodTruckJunkie.ApiServer_Security_Tests
{
    public class SixDigitNumbersFuzzDataAttribute : DataAttribute
    {
        private string _dataFilePath = "./fuzz-wordlists/6-digits-000000-999999.txt";

        public SixDigitNumbersFuzzDataAttribute()
        {

        }

        public SixDigitNumbersFuzzDataAttribute(string filePath)
        {
            _dataFilePath = filePath;
        }

        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            if (testMethod == null) { throw new ArgumentNullException(nameof(testMethod)); }

            string[] allLines = File.ReadAllLines(_dataFilePath);
            
            foreach(string s in allLines)
           {
               int value = int.Parse(s);
               yield return new object[] { value, value, value, value };
           }
        }
    }
}