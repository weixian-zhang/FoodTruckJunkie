import unittest
from unittest import mock, TestCase

#for debugging, debug os.path starts from "FoodTruckJunkie" folder
import sys, os

#this path for debug only
cliDir = os.path.abspath(os.path.join('src', 'FoodTruckJunkie.Cli'))
sys.path.append(cliDir)

#path import for unittest run
from pathlib import Path
basePath = os.path.abspath(os.path.join('')) #unittest run - os.patj starts from /FoodTruckJunkie/tests/unittests
unittestProjectPath = Path(basePath)
baseProjectPath = unittestProjectPath.parent.parent.parent
cliProjectPathFromUnitTestRun = os.path.join(baseProjectPath, 'src', 'FoodTruckJunkie.Cli')
sys.path.append(cliProjectPathFromUnitTestRun)

from api import Api

class ApiTest(TestCase):
    
    
    @mock.patch('api.Api.get_health', return_value='alive')
    def test_apiserver_get_health(self, mock_get_health):    
       
        status = mock_get_health()
            
        self.assertEqual(status, 'alive')
        
    @mock.patch('api.Api.search_nearest_food_trucks', return_value=[])
    def test_apiserver_search_nearest_food_trucks_with_correct_args(self, mock_search_nearest_food_trucks):    
       
        result = mock_search_nearest_food_trucks(37.79083842, -122.4012796, 5, 5)
            
        self.assertEqual(result, [])
        
            
        
if __name__ == '__main__':
    unittest.main()