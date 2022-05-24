## Food Truck Junkie

Food Truck Junkie is a [Web App](https://webapp-foodtruckjunkie-portal.azurewebsites.net) that helps you search for food trucks in San Francisco and displays them on an embedded Google Map for your ease of navigation to deliciousness. 

URLs below for quick testing  
*Note: web app is currently whitelisted (App Service IP Restrictions) for security reasons and only allow access when needed.
|  Components | URLs |
| ------------- | ------------- |
| Portal/Frontend  | https://webapp-foodtruckjunkie-portal.azurewebsites.net  |
| ApiServer/Backend  | https://webapp-foodtruckjunkie-api.azurewebsites.net/api/searchfoodtrucks?latitude=37.78798865&longitude=-122.3961007&distantMiles=20&noOfResult=1  |

## Project Description

The Web App aims to search for food truck coordinates (latitude, longitude) within a user-defined proximity in miles,  
by using the [Haversine Formula](https://en.wikipedia.org/wiki/Haversine_formula) on every food truck coordinates measured against user-defined coordinates entered in Frontend.  
Web App is fully hosted on Azure and consist of major components including
* a SPA-based Frontend developed using React
* Web API also known as ApiServer is an ASP.NET Core Backend that supports food truck search feature made by React Frontend.  
  In future ApiServer potentially adopts Backend-For-Frontend architectural pattern to support additional client types like Desktop App, mobile App and CLIs.
* Azure Database for MySQL as the main relational DB  
* other properties includes:
*  source of food truck dataset comes from [Mobile Food Facility Permit](https://data.sfgov.org/Economy-and-Community/Mobile-Food-Facility-Permit/rqzj-sfat/data)  
*  A food truck is plotted on GoogleMap only when their permit status is "APPROVED" 

#### Development Challenges
* A challenge with MySQL LIMIT clause is that LIMIT cannot be used with a variable for e.g LIMIT @numberOfResult as this syntax is considered invalid.
  A workaround could be to wrap the whole SELECT statement in string and use PREPARE stmt
  ![image](https://user-images.githubusercontent.com/43234101/169995446-3424ed5e-41b0-439a-9848-74df786660d3.png) 
  I solve this by simply setting paramter in ApiServer before passing parameter into MySQL stored procedure to calculate food truck proximity using Haversine formula.
* Frontend - when binding GoogleMap javascript object's "center" property to input textboxes of latitude and longitude over React-State,
  any text change causes map to grey-out.
  This is due to input textboxes are string type while GoogleMap's "center" property accepts decimal only.
  This is solved by parsing string to float e.g: parseFloat(this.state.latitude)
  
## Table of Content
* [Azure Deployment](#azure-deployment)
* [How to Contribute to Project](#how-to-contribute-to-project)
* [Software Architecture Design](#software-architecture-design)
  * [Context Diagram](#context-diagram)
  * [Container Diagram](#container-diagram)
  * [Component Diagram](#component-diagram)
* [Threat Modelling](#threat-modelling)
* [ApiServer Specifications](#api-server-specifications)
* [Database Specifications](#database-specifications)
* [Testings](#testings)
* Project Roadmap
* [What Have I Learned](#what-have-i-learned) - The Happy Moments :nerd_face:  

## Azure Deployment 

Refer to 

## How to Contribute to Project

## Software Architecture Design

### Context Diagram

### Container Diagram

### Component Diagram

## Threat Modelling

## Api Server Specifications

ApiServer subsystem is an ASP.NET Core 3.1 web application which uses Json as the default data response format.
* ApiServer adopts a URL Api versioning strategy as I wanted versioning to be explicit and not subtle like header and content-negotitation strategies.
  QueryString Api versioning strategt tends to clutter the Api's natural querystring parameters
* ApiServer also supports a /heath endpoint in addition to report "is alive" status, this endpoint can extend to return monitoring metrics modern monitoring tools like Prometheus to scrape metrics from.
* Supports OpenAPI to
  * improve external developers' experience
  * API Gateway products can easily import ApiServer's API spec using OpenApi Json format as exposed as ""/swagger/v1/swagger.json

| Verbs | API Paths | QueryStrings | Description | HTTP Status Code | Data Returned | 
| ------------- | ------------- | ------------- | ------------- | ------------- |  ------------- |
| GET | /api/<b>1.0</b>/searchfoodtrucks | latitude={decimal}&longitude={decimal}&distantMiles={int}&noOfResult={int} | search food truck info by given {latitude} + {longitude} within {distantMiles} | 200, 400 | "applicant": {string}, "foodItems": {string}, "latitude": {decimal}, "longitude": {decimal}, "address": {string},   "locationDescription": {string} |
| GET | /health |  | health status and monitoring information | 200 | |
| GET | / |  | OpenAPI/Swagger UI | 200 | |
| GET | /swagger/v1/swagger.json | | OpenAPI/Swagger Json | 200 | |


## Database Specifications

## Testings

## Project Roadmap

## What Have I Learned
