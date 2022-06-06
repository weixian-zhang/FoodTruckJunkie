import requests
import os
import json
from rich.console import Console

class Api:
    
    
    
    def __init__(self) -> None:
        self.baseApiUrl = os.environ.get('BASE_API_URL')
        self.apiUrl = 'api/1.0/'
        self.healthUrl = 'health/1.0/'
        self.verifyTLSCert = True
        self.searchFoodTruckUrl = 'searchfoodtrucks'
        self.console = Console()
        
        self.set_properties_by_env()
        
    
    def search_nearest_food_trucks(self, lat, long, distantMiles = 5, noOfResult = 5):
        
        try:
            url = os.path.join(self.baseApiUrl, self.apiUrl, self.searchFoodTruckUrl)
                    
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
            self.console.print(f'Error occured while searching nearest food trucks: {ex}', style='red')
            return []
        
    def get_health(self):
        try:
            
            url = os.path.join(self.baseApiUrl, self.healthUrl)
            
            response = requests.get(url, verify=self.verifyTLSCert)
            
            resultDict = json.loads(response.text)
            
            result =  resultDict['status']
            
            return result
            
        except Exception as ex:
            self.console.print(f'Error occured while getting health status: {ex}', style='red')
            return 'error'
        
    
    def set_properties_by_env(self):
        
        if self.baseApiUrl == None or self.isProd():
            self.baseApiUrl = 'https://webapp-foodtruckjunkie-api.azurewebsites.net/'
            
        if not self.isProd():
            self.verifyTLSCert = False 
            
    def isProd(self):
        env = os.environ.get('env')
        
        if env == None: #assume Prod when env not found
            return True
        
        if env.lower() == 'prod':
            return True
        
        return False