## Food Truck Junkie

A [Web App](https://webapp-foodtruckjunkie-portal.azurewebsites.net) that helps you search for food trucks in San Francisco and displays them on an embedded Google Map. 

URLs below for quick tetsing  
*Note the URLs are whitelisted for security reasons and only allowed access when needed.
|  Components | URLs |
| ------------- | ------------- |
| Portal/Frontend  | https://webapp-foodtruckjunkie-portal.azurewebsites.net  |
| ApiServer/Backend  | https://webapp-foodtruckjunkie-api.azurewebsites.net/api/searchfoodtrucks?latitude=37.78798865&longitude=-122.3961007&distantMiles=20&noOfResult=1  |

## Project Description

This project aims to search for food truck coordinates (latitude, longitude) within a user-defined proximity in miles,  
by using the [Haversine Formula](https://en.wikipedia.org/wiki/Haversine_formula) on every food truck coordinates against user-defined coordinates.  
Properties of this Web App includes:
*  source of food truck dataset comes from [Mobile Food Facility Permit](https://data.sfgov.org/Economy-and-Community/Mobile-Food-Facility-Permit/rqzj-sfat/data)  
*  A food truck is displayed on map only when their permit status is "APPROVED" 

This Web App is fully hosted on Azure and consist of major components including
* a SPA-based Frontend developed using React
* Web API also known as ApiServer is an ASP.NET Core b\Backend that supports food truck search feature made by Frontend,  
  and potentially also support future clients like desktop app, mobile apps and CLIs.
* Azure Database for MySQL as the main relational DB  

#### Development Challenges
* A challenge with MySQL LIMIT clause is that LIMIT cannot be used with a variable for e.g LIMIT @numberOfResult as this syntax is considered invalid.
  A workaround could be to wrap the whole SELECT statement in string and use PREPARE stmt
  ![image](https://user-images.githubusercontent.com/43234101/169995446-3424ed5e-41b0-439a-9848-74df786660d3.png) 
  I solve this by simply setting paramter in ApiServer before passing parameter into MySQL stored procedure to calculate food truck proximity using Haversine formula.
* Frontend - when binding GoogleMap javascript object's "center" property to input textboxes of latitude and longitude over React-State,
  any text change causes map to grey-out.
  This is due to input textboxes are string type while GoogleMap's "center" property accepts decimal only.
  This is solved by parsing string to float e.g: parseFloat(this.state.latitude)
