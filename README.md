## What is Food Truck Junkie

Food Truck Junkie is a [Web App](https://webapp-foodtruckjunkie-portal.azurewebsites.net) that helps you search for food trucks in San Francisco and displays them on an embedded Google Map for your ease of navigation to deliciousness. 

Quick test URLs below
*Note: web app is currently whitelisted (Azure App Service IP Restrictions) for security reasons and only allow access when needed.
|  Components | URLs |
| ------------- | ------------- |
| Portal/Frontend  | https://webapp-foodtruckjunkie-portal.azurewebsites.net  |
| ApiServer/Backend  | https://webapp-foodtruckjunkie-api.azurewebsites.net/api/1.0/searchfoodtrucks?latitude=37.78798865&longitude=-122.3961007&distantMiles=20&noOfResult=1  |

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
  
## Table of Content
* [System Deployment](#system-deployment)
* [How to Contribute to Project](#how-to-contribute-to-project)
* [Software Architecture Design](#software-architecture-design)
  * [Context Diagram](#context-diagram)
  * [Container Diagram](#container-diagram)
  * [Component Diagram](#component-diagram)
* [Security](#security)
* [ApiServer Specifications](#api-server-specifications)
* [Database Specifications](#database-specifications)
* [Testings](#testings)
* [Development Challenges](#development-challenges)
* [Project Roadmap](#project-roadmap)
* [What Have I Learned](#what-have-i-learned) - The Happy Moments :nerd_face:  

## System Deployment & Azure Resource provisioning

The primary target deployment platform is on Azure and PaaS services are preferred over IaaS, as much as possible to reduce any infra-related maintenance overhead.
* Infra-as-Code (see [Project Roadmap]((#project-roadmap))) is the defacto means to deploy any Azure resource, we are open to any practical ways but the team mainly adopts Bicep, Terraform and/or PowerShell at times.
* for Apps deployment, our team is working towards GitHub Actions to perform end-to-end DevSecOps (see [Security](#security)). Currently we use [VSCode Azure Tools extension](https://marketplace.visualstudio.com/items?itemName=ms-vscode.vscode-node-azure-pack) for deployment.

| Sub-Systems | Azure Resource | Descriptions |
| ------------- | ------------- | ------------- |
| Portal | App Service | | |
| Api Server | App Service | configurations are stored in App Service - Configurations, all properties are encrypted-at-rest by default |
| Database | Azure Database for MySQL | | |

## How to Contribute to Project

### Branching Strategy

Our team practice Feature Branching, any feature is a short-lived branch that you can clone from Dev.
Once your great work is done, do a pull request and find any team mate for a second pair of eyes before merging into Dev.
We like to work on bite-size feature, so it is important how the User Stories are broken down into "manageable-sized" Tasks and each Task should do only one thing.
For example implementing a Search-FoodTruck-By-FoodType User Story, updating ApiServer, Service and Repository Layers can be one task. Updating Stored Proc to accept "FoodType" parameter can be another task, lastly updating Portal can be another. This User Story can be a branch.  
<img src="https://user-images.githubusercontent.com/43234101/170171766-8aba42ff-a88c-4ab0-8d18-2fb58447d50d.png" width="600" height="400" />  
[A Workbench diagram](https://www.azureworkbench.com/?id=RKytBbvBNVqtAtxSXfJg)

* Dev branch will be deployed to Staging environment to perform various tests before merging into Main over pull request
* Main branch is for production release only. 


### Bug Tracker using GitHub Issues
A bug report template tends to look like:
```Great Bug Reports tend to have:

A quick summary and/or background
Steps to reproduce
Be specific!
Give sample code if you can.
What you expected would happen
What actually happens
Notes (possibly including why you think this might be happening, or stuff you tried that didn't work)
```

## Software Architecture Design

For describing software architecture, we like to use the [C4 Model](https://c4model.com/) modelling technique. We reckon that thete are other mature techniques like [4+1 View Models]([url](https://en.wikipedia.org/wiki/4%2B1_architectural_view_model)) with UML, we favour C4 due to its lean and simple nature for both non-technical and technial folks.
The following diagrams describe Food Truck Junkie's software architecture from a bird's eye-view using Context Diagram, zooming into the Infra and Network architecture using Container Diagram 

### Context Diagram

<img src="https://user-images.githubusercontent.com/43234101/170187803-b1a55b76-dda0-4ac9-9511-350539bfe478.png" width="200" height="500" />  
A big picture of how users use FoodTruckJunkie Web App


### Container Diagram

![image](https://user-images.githubusercontent.com/43234101/170189736-928789aa-5c3a-4e48-a627-4e2cfff2a802.png)

[A Workbench diagram](https://www.azureworkbench.com/?id=jSjmXFhgHxBzFuBTUTap)

### Component Diagram

## Security

## Api Server Specifications

ApiServer subsystem is an ASP.NET Core 3.1 web application which uses Json as the default data response format.
* ApiServer adopts a URL Api versioning strategy as I wanted versioning to be explicit and not subtle like header and content-negotitation strategies.
  QueryString Api versioning strategt tends to clutter the Api's natural querystring parameters
* ApiServer also supports a /heath endpoint in addition to report "is alive" status, this endpoint can extend to return monitoring metrics modern monitoring tools like Prometheus to scrape metrics from.
* Supports OpenAPI to
  * improve external developers' experience
  * API Gateway products can easily import ApiServer's API spec using OpenApi Json format as exposed as ""/swagger/v1/swagger.json

| Verbs | API Paths | QueryStrings | Description | Status Code | Data Returned | 
| ------------- | ------------- | ------------- | ------------- | ------------- |  ------------- |
| GET | /api/<b>1.0</b>/searchfoodtrucks | latitude={decimal}&longitude={decimal}&distantMiles={int}&noOfResult={int} | search food truck info by given {latitude} + {longitude} within {distantMiles} | 200, 400 | "applicant": {string}, "foodItems": {string}, "latitude": {decimal}, "longitude": {decimal}, "address": {string},   "locationDescription": {string} |
| GET | /health |  | health status and monitoring information | 200 | |
| GET | / |  | OpenAPI/Swagger UI | 200 | |
| GET | /swagger/v1/swagger.json | | OpenAPI/Swagger Json | 200 | |


## Database Specifications

## Testings

## Development Challenges
* A challenge with MySQL LIMIT clause is that LIMIT cannot be used with a variable for e.g LIMIT @numberOfResult as this syntax is considered invalid.
  A workaround could be to wrap the whole SELECT statement in string and use PREPARE stmt
  <img src="https://user-images.githubusercontent.com/43234101/169995446-3424ed5e-41b0-439a-9848-74df786660d3.png" width="700" height="250" />  
  An easier way to solve this is by setting "PageSize" in ApiServer before passing parameter into MySQL stored procedure to calculate food truck proximity using Haversine formula.
* Frontend - when binding GoogleMap javascript object's "center" property to input textboxes of latitude and longitude over React-State,
  any text change causes map to grey-out.
  This is due to input textboxes are string type while GoogleMap's "center" property accepts decimal only.
  This is solved by parsing string to float e.g: parseFloat(this.state.latitude)

## Project Roadmap

## What Have I Learned

The idea of this one-person-sized challenge is an interesting one, most importantly this challenge can effectively access one's knowledge and experience in a full Software Delivery Lifecycle end to end from understanding requriements, to software architecture design, system Infra/Networking design, threat modelling, software module-level design (package-level), software development in full-stack involving polyglot languages and frameworks from Frontend, Backend to Database, to deployment in this case Azure, and finally to documentation.
