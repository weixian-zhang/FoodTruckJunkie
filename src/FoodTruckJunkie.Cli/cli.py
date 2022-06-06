from rich.console import Console
import typer
from typing import Optional
from api import Api
from rich.console import Console
from foodtrucks_commands import app as foodtrucks_app

api = Api()

console = Console()
    
CONTEXT_SETTINGS = dict(help_option_names=['-h', '--help'])

app = typer.Typer(context_settings=CONTEXT_SETTINGS, add_completion=False)

app.add_typer(foodtrucks_app, name='foodtrucks') #add sub commands for future additional of foodtruck related cmds like List, Get and etc

@app.command()
def health():
    status = api.get_health()
    
    if status == 'alive':
        console.print('ApiServer is alive!', style='green')
    

def run():
    app()


    
    
       