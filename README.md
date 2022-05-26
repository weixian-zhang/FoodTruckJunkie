## What is Food Truck Junkie

Food Truck Junkie is a [Web App](https://webapp-foodtruckjunkie-portal.azurewebsites.net) that helps you search for food trucks in San Francisco and displays them on an embedded Google Map for your ease of navigation to deliciousness, but mostly for hunger. 

Quick test URLs below  
*Note: web app is currently whitelisted (Azure App Service IP Restrictions) for security reasons and only allow access when needed.
|  Components | URLs |
| ------------- | ------------- |
| Portal/Frontend  | https://webapp-foodtruckjunkie-portal.azurewebsites.net  |
| ApiServer/Backend  | https://webapp-foodtruckjunkie-api.azurewebsites.net/api/1.0/searchfoodtrucks?latitude=37.78798865&longitude=-122.3961007&distantMiles=20&noOfResult=1  |

## Project Description

This web app aims to search for food truck coordinates (latitude, longitude) within a user-defined proximity in miles,  
by using the [Haversine Formula](https://en.wikipedia.org/wiki/Haversine_formula) on every food truck coordinates measured against user-defined coordinates entered in Frontend.  
Web App is fully hosted on Azure and consist of major components including
* a SPA-based Frontend developed using React
* Web API also known as ApiServer is an ASP.NET Core Backend that supports food truck search feature made by React Frontend.  
* Azure Database for MySQL as the main relational DB  
* other properties includes:
  *  source of food truck dataset comes from [Mobile Food Facility Permit](https://data.sfgov.org/Economy-and-Community/Mobile-Food-Facility-Permit/rqzj-sfat/data)  
  *  A food truck is retrieved and plotted on map only when it's permit status is "APPROVED" 
  
## Table of Content
* [System Deployment & Azure Resource Provisioning](#system-deployment--azure-resource-provisioning)
* [How to Contribute to Project](#how-to-contribute-to-project)
* [Software Architecture Design](#software-architecture-design)
  * [Context Diagram](#context-diagram)
  * [Container Diagram](#container-diagram)
  * [Component Diagrams](#component-diagrams)
* [Security In Software Development](#security-in-software-development)
* [ApiServer Specifications](#api-server-specifications)
* [Database Specifications](#database-specifications)
* [Testings](#testings)
* [Development Challenges](#development-challenges)
* [Project Roadmap - If I Have More Time](#project-roadmap---if-i-have-more-time)
* [What I Have Learned](#what-i-have-learned) - The Happy Moments :nerd_face:  

<br />
<br />


## System Deployment & Azure Resource Provisioning

Azure is our hosting platform and PaaS services are preferred, over IaaS as much as possible to reduce any infra-related maintenance overhead.
* Infra-as-Code (see [Project Roadmap]((#project-roadmap))) is the defacto means to deploy any Azure resource, we are open to any practical ways but the team mainly adopts Bicep, Terraform and/or PowerShell at times.
* for apps and IAC scripts deployment, in Project Roadmap, DevSecOps (see [Security In Software Development](#security-in-software-development)) will be implemented using Azure DevOps Pipelines or GitHub Actions.  
  As for now we use [VSCode Azure Tools extension](https://marketplace.visualstudio.com/items?itemName=ms-vscode.vscode-node-azure-pack) for deployment.

| Sub-Systems | Azure Resource | Descriptions |
| ------------- | ------------- | ------------- |
| Portal | App Service | | |
| Api Server | App Service | configurations are stored in App Service - Configurations, all properties are encrypted-at-rest by default |
| Database | Azure Database for MySQL | | |

<br />
<br />

## How to Contribute to Project

### Branching Strategy

Our team practices GitFlow branching, any feature is a short-lived branch that you can clone from Dev.
Once your great work is done, do a pull request and find any team mate for a second pair of eyes before merging into Dev.
We like to work on bite-size feature, so it is important how the User Stories are broken down into "manageable-sized" Tasks and each Task should only do one thing.  
>For example implementing a Search-FoodTruck-By-FoodType User Story, updating ApiServer, Service and Repository Layers can be one task. Updating Stored Proc to accept "FoodType" parameter can be another task, lastly updating Portal can be another. This User Story can be a branch.  

* Dev branch will be deployed to Staging environment to perform various tests before merging into Main over pull request
* Main branch is for production release only
  * Every production release is a new branch
  * Any production bug fix will be a new branch from the release-branch
  * Bug fixes will be merged back to release-branch, Main and Dev

[Feature Toggling](https://github.com/launchdarkly/featureflags/blob/master/6%20-%20Flags%20vs%20Branching.md) - there is a disadvantage to GitFlow and that is many branches to maintain, and the merge back to Main and Dev can be complicated.
One way to tackle this issue is to introduce Feature Toggling (a.k.a feature flipping)

<img src="https://user-images.githubusercontent.com/43234101/170193365-0c6eb4eb-0c3f-44a2-b075-c7206d00581a.png" width="800" height="400" />  

[A Workbench diagram](https://www.azureworkbench.com/?id=RKytBbvBNVqtAtxSXfJg)


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
<br />
<br />

## Software Architecture Design

For describing software architecture, we like to use the [C4 Model](https://c4model.com/) modelling technique. We reckon that there are other mature techniques as well like [4+1 View Models](https://en.wikipedia.org/wiki/4%2B1_architectural_view_model)) and UML. We favour C4 due to its lean and simple nature for both non-technical and technial folks.  
The following [diagrams](https://github.com/weixian-zhang/FoodTruckJunkie/blob/main/docs/FoodTruckJunkie-software_architecture_design.pptx) describe Food Truck Junkie's software architecture from a high-level than zooming into more design details on subsequent diagrams.

### Context Diagram

A big picture of how users use FoodTruckJunkie Web App  
<img src="https://user-images.githubusercontent.com/43234101/170187803-b1a55b76-dda0-4ac9-9511-350539bfe478.png" width="250" height="500" />  

### Container Diagram

* ApiServer is RESTful API hosted on multi-tenanted App Service.  
  In Project Roadmap, for security reasons, Azure FrontDoor WAF (see [project roadmap](#project-roadmap)) can be added in-front of ApiServer to allow FrontDoor WAF       capability to perform SSL Termination and Owasp web vulnerability scan, before routing scanned HTTP traffic to ApiServer. 
* SPA-based Portal is hosted on a separate App Service. In future, Portal can be moved to Azure Static Web App to take advange of the built-in CDN capability.
![image](https://user-images.githubusercontent.com/43234101/170195884-acccc7a8-ebd3-4248-8a6a-5d6a5ee24937.png)

[A Workbench diagram](https://www.azureworkbench.com/?id=jSjmXFhgHxBzFuBTUTap)

### Component Diagrams

#### Layered Architecture Diagrams
A supplementary Layered architecture diagram is added to explicitly describe the adoption of a Layered architectural style. I feel this diagram can better help developers to visualize the packages used in each Layers and the Cross-Cutting Concerns packages.

<img src="https://user-images.githubusercontent.com/43234101/170198413-068eaedf-93e2-4954-87b5-7f4d03a24226.png"  width="600" height="550" />  

#### Component Interaction Diagram

* API Controller layer (see [Api Specifications](#api-specifications)) is the entry point to Web App and it contains all package dependencies including cross-cutting (concerns) libraries like Serilog (logging framework). Dependency Injection is configured here to map Interfaces with their concrete classes
* With reference to the [12 Factor App](https://12factor.net/) guiding principles, environment variables 

<img src="https://user-images.githubusercontent.com/43234101/170208325-93a86940-294c-434d-9c44-6b13bc3ac29a.png" width="1100" height="600" />  

## Security In Software Development

The following are software security practices our team strives to following throughout the SDLC of this project

### Threat Modelling

* Threat modelling was done according to [Container Diagram](#container-diagram) and the designer file can be found [here](https://github.com/weixian-zhang/FoodTruckJunkie/blob/main/docs/ThreatModel-FoodTruckJunkie.tm7).  
Threat Modelling Report can be found [here](https://github.com/weixian-zhang/FoodTruckJunkie/blob/main/docs/ThreatMode-Report-FoodTruckJunkie%20WebApp.htm)
* The detected threats were evaluated, for each threat mark with "Needs Investigation", a work item will be created to further explore mitigations in the form of code-based enhancements, configuration changes to existing Azure resource, and/or by adding new security-related Azure resources or 3rd-party COTS products if necessary. 

### Authentication

In the future Project Roadmap, we will want users to be able to sign-in with their Microsoft personal accounts (a.k.a Live account.
Behind the scenes, we will be implementing OpenID Connect authentication on Portal and ApiServer.
* SPA authn: Portal will use the  OAuth Authorization Code Flow + PKCE as it is a Public Client.
* API authn: ApiServer being a Confidential Client will be using Auhorization Code Flow
* API authn-chain : if at that point when Food Truck Junkie has expanded to have other features which are implemented as microservices, and has the requirement to       for microservices to microservices API authentication, we will then be implementing Authorization Code Flow-On-Behalf Flow for API-to-API authentication chain         scenario.
  The concept is when an API (API-A) receive an access token from Portal, API-A uses the access token and with its ClientID/Secret, it exchange for another access       token Token-A. This Token-A will be pass on to API-B for authentication. Within Azure AD we will need to configure "Expose API" to add OAuth scopes for each           microservices. Then followed by configuring authorization in Azure AD - App - "API Permissions", specifiying which API scopes are allowed access to which               microservices.

### Azure Development Security Guidelines
* Always use Azure Managed Identity (wherever supported) as the authentication mechaism when accessing Azure services
* All Secrets, Asymmetric and Symmetric keys and x.509 Certifications should store in Azure Key Vault
* Application Insights SDK should always be implemented for supported languages. Always enable Application Insights that has direct integration with the Azure web app   hosting services.
  Although Application Insights is a App Performance Monitoring (APM) tool
   * the App Map feature could show if web app is contacting any suspicious external endpoints, detecting malware doing Commmand and Control to form backdoors and          executing data exfiltration
   *  App Insight logs could potential, further joined with other log types in Azure Sentinel if need be
   
### During Pre-Commit Stage (while you are coding)

* Secure coding practices - we follow these [secure coding practice checklists](https://owasp.org/www-pdf-archive/OWASP_SCP_Quick_Reference_Guide_v2.pdf) while we code
  Example: > * do not disclose sensitive info in error message
	     * close DB connection right after use, leaving  persisted TCP connections are like leaving a back-door for adversaries to exploit
             * validate all inputs
             * encode all outputs including HTML, Javascript, CSS, XML, JSON, Http Headers and more
           
* App secrets - if develope using .Net use the [User Secret](https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-6.0&tabs=windows) feature from SDK 
* Pre-commit hook - use Git pre=commit hook to prevent committing secrets
* DevSkim VSCode extension - able to provide inline security assessment while writing codes on the fly. Example: using outdated Hash MD5 library.
* Security peer review - security reviews can be done on high risk modules such as authentication, authorization, user management, cryptography and etc.  
  Also based on secure coding practice checklists
* Unit Testing - Aim for at least 90% of code coverage when writing unit test cases. Unut tests should also cover non-happy flows  
  
 ### Security In DevOps Pipelines
 
 We will be introducing security tasks into our Build and Release pieplines as follows
 * <b>Build Pipeline</b>
    * Credential Scanner - detects credentials and secrets
    * SonarQube Static Code Analysis - code smells, bugs and security vulnerabilities ([rules here](https://docs.sonarqube.org/latest/user-guide/security-rules/))
    * WhiteSource Bolt - detects vulnerabilities in open source components and provides fixes and checks licensing
    * OWasp Dependency Checks - detects publicly disclosed vulnerabilities contained within a project’s dependencies
    * Anchore Image Scanner - For scanning Docker images for vulnerabilities, if any team member decides to containerize some sub-systems
    * Run Unit Tests
    
 * <b>Release Pipeline</b>
    * OWASP ZAP - integrated penetration testing tool to detect web vulnerabilities in web apps
    * Fuzz Test - explore the introduction of Fuzzing with web fuzzers like [FFUS](https://github.com/ffuf/ffuf) or [OneFuzz](https://github.com/microsoft/onefuzz/blob/main/README.md)
     
<br />
<br />

## Api Server Specifications

ApiServer subsystem is an ASP.NET Core 3.1 web application which uses Json as the default data response format.
* ApiServer adopts a URL Api versioning strategy as we want versioning to be explicit and not subtle like header and content-negotitation strategies.
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

<br />
<br />

## Database Specifications

We are using Azure Database for MySQL as our amin relational DB.
All DB setup scripts can be found [here](https://github.com/weixian-zhang/FoodTruckJunkie/tree/main/src/Scripts/DB).
The scripts include:
* data loading script that reads from data CSV file
* table creation script
* stored procedure that uses the Haversine formula to calculate the distant between filtered food truck coordinates, against coordinates passed in as paramaters from     web portal.

<br />
<br />

## Testings

The current available tests are Unit Tests written using XUnit framework, unit tests covers all 3 layers including API Controller, Service and Repository.
Additionally, Moq library is used to Mock a DB C# Interface so that MySQL DB access can be mocked and not include any integration to MySQL.
We are able to make mocking happen due to consistent usage of Intefaces, and all concrete classes must implement Interfaces.
Dependencies are always constructor injected and codes need to refactor to support unit testing if needed.

In our project roadmap, we plan to setup the following tests:
* Automated Smoke Tests
  * Portal: using [Cypress](https://docs.cypress.io/guides/overview/why-cypress#Setting-up-tests), [Selenium](https://www.selenium.dev/documentation/) or similar Web       UI testing tool to automate UI testing. Smoke test still follows a set of test cases consisting of       test steps, however, Smoke Test aims to test if a build is     stable and covers breadth more than depth.
  * ApiServer: using [Artillery](https://www.artillery.io/docs/guides/getting-started/core-concepts#test-definitions) to smoke test ApiServer by pre-configuring the querystring parameters following a set of defined test cases.
* Load Tests - [Artillery](https://www.artillery.io/docs/guides/getting-started/core-concepts#test-definitions) can also be used for load testing by increaing the       number of "virtual-users"
* Fuzz Test - As mentioned, we could explore Grammer-based Fuzzing to generate templatized inputs to Web APIs to effectively find bugs and security vulnerability.

<br />
<br />

## Development Challenges
* A challenge with MySQL LIMIT clause is that LIMIT cannot be used with a variable for e.g LIMIT @numberOfResult as this syntax is considered invalid.
  A workaround could be to wrap the whole SELECT statement in string and use PREPARE stmt
  <img src="https://user-images.githubusercontent.com/43234101/169995446-3424ed5e-41b0-439a-9848-74df786660d3.png" width="700" height="250" />  
  An easier way to solve this is by setting "PageSize" in ApiServer before passing parameter into MySQL stored procedure to calculate food truck proximity using Haversine formula.
* Frontend - when binding GoogleMap javascript object's "center" property to input textboxes of latitude and longitude over React-State,
  any text change causes map to grey-out.
  This is due to input textboxes are string type while GoogleMap's "center" property accepts decimal only.
  This is solved by parsing string to float e.g: parseFloat(this.state.latitude)

<br />
<br />

## Project Roadmap - If I Have More Time

* Software Patterns & Practices	
  * Backend For Frontend pattern - in future if more client types like Desktop, mobile and Cli are added, we could explore Backend-For-Frontend (BFF) pattern.
    How BFF works is that there will be a API layer where data and business logic are customized for each Frontend type.
    These BFF client-specific APIs are usually coarse-grain APIs that could be calling other microservices to aggregate data before returning to the client.
    For example, Mobile App may not have an admin module while Desktop and Web clients have it. Hence Mobile BFF API will not contain admin modules as well.
    
  * Adopts the Microservices architectural style as system grows. This will be a brown-field migration to Microservices archietcture.
    Each Bounded Context of the system domain can be an independent microservice maintained by a separate team with their own programming language and tech-stack of       their choice.
    
* Technology
  * Microservices are deployed on Azure Kubernetes private cluster. Service Mesh like Istio can be introduce for API Gateway capability, mutal-TLS microservice o           microservice authn, and network policy built-in to contract network reacbility amongst microservices and more.
  * A data loading daemon app implemented using Azure Function to automatically and timely pull Food Truck permit data file and refresh static data
  * Use Azure Static Web App to host Portal instead of App Service so as to take advantage of built-in CDN and DevOps deployment features.
  * Cache search result in Azure Redis using Cache-Aside pattern to return queried result faster
  * Develope Azure Bicep scripts to setup the Azure environment
  * Azure FrontDoor WAF added in-front of ApiServer for security coverage:
    * Custom Rule to perform rate limiting
    * OWASP TOp 10 web vulnerability scanning on TLS terminated traffic, before routing to ApiServer

* Functional
  * Supporting Address searching - search nearby food trucks by Address in addition to coordinates
  * Search by Food
    * Food type data can be isolated and compiled and save into a dedicated DB Table
    * In the Frontend, food type data can be retrieved when web app loads and food type can be cached in browser's local storage
    * Lastly, an auto-complete textbox is added and binds to the cached food type data. As user types in auto-complete box, the user will be actively prompted with           available food type. This makes food truck searching experience
    * using Azure Boards to track user stories and work items
 
<br />
<br />

## What I Have Learned

The idea of this challenge is an interesting one, most importantly this challenge can effectively get to know one's knowledge and experience in a full Software Delivery Lifecycle. Starting from understanding the requriements, to software architecture design, system Infra/Networking architecture design, threat modelling, software module-level design (package-level), software development in full-stack involving polyglot languages and frameworks from Frontend, Backend to Database, to deploying system to Azure (in this case), and finally to documentation.
In a real-world software project, we have many other participating roles like Business Analysts for requirements gathering and documentation, Software Engineers, Software Architects, Software Tech Leads, System Admins to OS/Infra/Networking setup and configuration, DBA to setup and configure DB clusters (and even run our TSQL scripts some times). Test specialists using specalized software to run penetration tests against our systems.
3rd-party security auditors to scrutinize our OS hardening configurations, firewall rules, network routing, web server configurations and more.
And not forgetting Project Manager, Scrum Master, Subject-Matter Experts and Product Specialists of certain line-of-products.

Having said a mouthful, this challenge although a one-person size project, it can put one through most of the SDLC stages and playing different roles all at once, simulating a real-world project. I really enjoyed myself.

* Before I start this challenge, I took several hours to learn about Latitude, Longitude, the geographical East-West reference line of Greenwich Meridian,
  and how to plot coordinates on a world map.
  This may be common sense knowledge to other people but for me, I just didn't manage to learn before this challenge. I am glad I did now.
* I always wanted to learn how to embed maps into an App and have the icons on the map move by itself. The idea of tracking someone or device on a map watching it move   is really fun. Finally got a purposeful geospatial requirement to try Map technology. I wanted to use Azure Map at first but due to there are many open-source         libraries on integrating Google Maps into a React web app, and most importantly in the essence of time, I chose Google Map.
* I also chose MySQL because I have not work on MySQL before. Thanks to that I am able to discover a very common usage of LIMIT clause but yet with big limitation in     my opinion. (see [Development Challenges](#development-challenges)).
