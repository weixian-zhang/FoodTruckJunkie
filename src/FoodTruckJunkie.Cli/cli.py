import typer
from typing import Optional
from api import Api
import json
import click

class FoodTruckJunkieCli:
    
    api = Api()
    
    api = Api()
    
    CONTEXT_SETTINGS = dict(help_option_names=['-h', '--help'])

    app = typer.Typer()

    foodtrucks_app = typer.Typer()
    app.add_typer(foodtrucks_app, name='foodtrucks')
        

    @foodtrucks_app.command("search")
    def foodtrucks(lat: float, long: float, distantMiles: int = 5, noOfResults: int = 5):
        
        nearestFoodTrucks = FoodTruckJunkieCli.api.search_nearest_food_trucks(lat, long, distantMiles, noOfResults)
        
        jsonStr = json.dumps(nearestFoodTrucks, sort_keys=True, indent=4)
        
        typer.echo(jsonStr)
       
    
    def run(self):
        FoodTruckJunkieCli.app()
       