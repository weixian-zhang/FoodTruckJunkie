import time
from rich.console import Console

console = Console()

def log_time(func):
    def wrapper(*args, **kwargs):
        start = time.time()
        val = func(*args, **kwargs)
        end = time.time()
        total = end - start
        console.print(f'"{func.__name__}" took {str(round(total,1))} milisecs to complete')
        return val
        
    return wrapper