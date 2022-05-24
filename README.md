### Food Truck Junkie
A [Web App](https://webapp-foodtruckjunkie-portal.azurewebsites.net) that helps you search for food trucks in San Francisco and displays them on an embedded Google Map.  

### Project Description  

This project aims to search for food truck coordinates within a proximity calculated using 
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

Frontend has input fields for user to enter their current coordinates(latitude, longtitude) and perform a search on nearby available food trucks and together,  
Portal displays user's current location and food trucks in Google Map.
*  source of food truck dataset comes from [here](https://data.sfgov.org/Economy-and-Community/Mobile-Food-Facility-Permit/rqzj-sfat/data)
*  A food truck is displayed in map only when their permit status is "APPROVED"  

This is an important component of your project that many new developers often overlook.

Your description is an extremely important aspect of your project. A well-crafted description allows you to show off your work to other developers as well as potential employers.

The quality of a README description often differentiates a good project from a bad project. A good one takes advantage of the opportunity to explain and showcase:

What your application does,
Why you used the technologies you used,
Some of the challenges you faced and features you hope to implement in the future.
