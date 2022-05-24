## Food Truck Junkie

A [Web App](https://webapp-foodtruckjunkie-portal.azurewebsites.net) that helps you search for food trucks in San Francisco and displays them on an embedded Google Map.  

## Project Description

This project aims to search for food truck coordinates (latitude, longitude) within a user-defined proximity in miles,  
by using the [Haversine Formula](https://en.wikipedia.org/wiki/Haversine_formula) on every food truck cooridinate against user-defined corrdinates.  
Properties of this Web App includes:
*  source of food truck dataset comes from [Mobile Food Facility Permit](https://data.sfgov.org/Economy-and-Community/Mobile-Food-Facility-Permit/rqzj-sfat/data)  
*  A food truck is displayed on map only when their permit status is "APPROVED" 

This Web App is fully hosted on Azure and consist of major components including
* a SPA-based Frontend developed using React
* Web API also known as ApiServer is an ASP.NET Core b\Backend that supports food truck search feature made by Frontend,  
  and potentially also support future clients like desktop app, mobile apps and CLIs.
* Azure Database for MySQL as the main relational DB  

For quick testing you can access the Frontend and Backend below  
*Note the URLs are whitelisted for security reasons and only allowed access when needed.
|  Components | URLs |
| ------------- | ------------- |
| Frontend  | https://webapp-foodtruckjunkie-portal.azurewebsites.net  |
| ApiServer  | https://webapp-foodtruckjunkie-api.azurewebsites.net/api/searchfoodtrucks?latitude=37.78798865&longitude=-122.3961007&distantMiles=20&noOfResult=1  |

#### Development Challenges
* A challenge on MySQL LIMIT clause is that LIMIT cannot be used with a variable for e.g LIMIT @numberOfResult as this syntax is considered invalid.
  A workaround could be to wrap the whole SELECT statement in string and use PREPARE stmt
  ![image](https://user-images.githubusercontent.com/43234101/169995446-3424ed5e-41b0-439a-9848-74df786660d3.png)


This is an important component of your project that many new developers often overlook.

Your description is an extremely important aspect of your project. A well-crafted description allows you to show off your work to other developers as well as potential employers.

The quality of a README description often differentiates a good project from a bad project. A good one takes advantage of the opportunity to explain and showcase:

What your application does,
Why you used the technologies you used,
Some of the challenges you faced and features you hope to implement in the future.
