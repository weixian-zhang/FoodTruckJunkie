import requests
import os
import json

class Api:
    
    def __init__(self) -> None:
        self.baseApiUrl = os.environ.get('BASE_API_URL')
        self.verifyTLSCert = True
        self.searchFoodTruckUrl = 'searchfoodtrucks'
        
        if self.baseApiUrl == None:
            self.baseApiUrl = 'https://webapp-foodtruckjunkie-api.azurewebsites.net/api/1.0/'
            
        if os.environ.get('env') != 'prod':
            self.verifyTLSCert = False
        
    
    def search_nearest_food_trucks(self, lat, long, distantMiles = 5, noOfResult = 5):
        
        try:
            url = os.path.join(self.baseApiUrl, self.searchFoodTruckUrl)
                    
            params = {
                'latitude': lat,
                'longitude': long,
                'distantMiles': distantMiles,
                'noOfResult': noOfResult
            }
            
            response = requests.get(url, params=params, verify=self.verifyTLSCert)
            
            resultStr = response.text
            
            resultDic = json.loads(resultStr)
            
            nearestFoodTrucks = resultDic['nearestFoodTrucks']
            
            return nearestFoodTrucks
        except Exception as ex:
            print(ex)
            return []