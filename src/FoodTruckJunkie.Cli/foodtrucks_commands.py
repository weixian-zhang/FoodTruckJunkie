from click import help_option
import typer
import json
from api import Api
from rich.console import Console

api = Api()

console = Console()

CONTEXT_SETTINGS = dict(help_option_names=['-h', '--help'])

app = typer.Typer(name='foodtrucks', help='foodtrucks commands', context_settings=CONTEXT_SETTINGS)

@app.command('search')
def search(lat: float = typer.Argument('37.79183426', help='latitude'), 
               long: float = typer.Argument('-122.4012796', help='longitude'), 
               proximity: int = typer.Argument(5, help="food trucks proximity in miles"), 
               noofresults: int = typer.Argument(5, help="number of results returned")):
    
    nearestFoodTrucks = api.search_nearest_food_trucks(lat, long, proximity, noofresults)
    
    jsonResult = json.dumps(nearestFoodTrucks, sort_keys=True, indent=4) #prettify with indents
    
    console.print(f'total result: {len(nearestFoodTrucks)}', style="green")
    console.print(jsonResult, style="green")
    
    