import axios from "axios";

export default class FoodTruckService
{
    searchNearestFoodTruck(latitude, longitude, distantMiles, noOfResult, onSuccess, onFailure) {
        
        axios.get('api/1.0/searchfoodtrucks', 
            {
              params: {
                latitude: latitude,
                longitude: longitude,
                distantMiles: distantMiles,
                noOfResult: noOfResult
              },
              headers: {
                'Content-Type': 'application/json'
              }
            })
            .then(function (response) {
              var result = response.data;
              onSuccess(result);
            })
            .catch(function (error) {
              console.log(error);
              onFailure(error);
            });
    }
}