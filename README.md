## What is Food Truck Junkie

Food Truck Junkie is a [Web App](https://webapp-foodtruckjunkie-portal.azurewebsites.net) that helps you search for food trucks in San Francisco and displays them visuallu on a map for your ease of navigation to deliciousness. 

Quick test URLs below  
*Note: web app is currently whitelisted (Azure App Service IP Restrictions) for security reasons and only allow access when needed.
|  Components | URLs |
| ------------- | ------------- |
| Portal/Frontend  | https://webapp-foodtruckjunkie-portal.azurewebsites.net  |
| ApiServer/Backend  | https://webapp-foodtruckjunkie-api.azurewebsites.net/api/1.0/searchfoodtrucks?latitude=37.78798865&longitude=-122.3961007&distantMiles=20&noOfResult=1  |
| OpenAPI Portal | https://webapp-foodtruckjunkie-api.azurewebsites.net |
| OpenAPI Json | https://webapp-foodtruckjunkie-api.azurewebsites.net/swagger/v1/swagger.json |
| ApiServer Health endpoint | https://webapp-foodtruckjunkie-api.azurewebsites.net/health/1.0 |  

[![Food Truck Junkie](https://user-images.githubusercontent.com/43234101/170927395-5df23ba5-f36f-43f3-9c62-a86f92cf16f8.png)](https://webapp-foodtruckjunkie-portal.azurewebsites.net)

<br />

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
  *  Food truck result min 5, max 50

<br />
<br />

## Table of Content
* [System Deployment & Azure Resource Provisioning](#system-deployment--azure-resource-provisioning)
* [How to Contribute to Project](#how-to-contribute-to-project)
* [Software Architecture Design](#software-architecture-design)
  * [Context Diagram](#context-diagram)
  * [Container Diagram](#container-diagram)
  * [Component Diagrams](#component-diagrams)
* [AppSec](#appsec)
   * [AppSec Hobby Projects](#appsec-hobby-projects)
   * [AppSec Practices in SDLC](#appsec-practices-in-sdlc)
   * [AppSec Training & Awareness | SDLC - Planning Phase](#appsec-training--awareness--sdlc---planning-phase)
   * [Define Security Requirements | SDLC - Define Requirement Phase](#define-security-requirements--sdlc---define-requirement-phase)
   * [Threat Modelling | SDLC - Design & Protoyping Phase](#threat-modelling--sdlc---design--protoyping-phase)
   * [Secure Coding | SDLC - Development Phase](#secure-coding--sdlc---development-phase)
   * [Security Unit Tests | SDLC - Development Phase](#security-unit-tests--sdlc---development-phase)
   * [Secure Code Review | SDLC - Development Phase](#secure-code-review--sdlc---development-phase)
   * [DevSecOps | SDLC - Deployment Phase](#devsecops--sdlc---deployment-phase)
   * [Authentication](#authentication)
   * [Authorization](#authorization)
   * [AppSec Guidelines in Azure Software Development](#appsec-guidelines-in-azure-software-development)
* [ApiServer Specifications](#api-server-specifications)
* [Database Specifications](#database-specifications)
* [Logging & Monitoring](#logging--monitoring)
* [Testings](#testings)
* [Development Challenges](#development-challenges)
* [Project Roadmap - If I Have More Time](#project-roadmap---if-i-have-more-time)
* [What I Have Learned](#what-i-have-learned)

<br />
<br />


## System Deployment & Azure Resource Provisioning

Azure is our hosting platform and PaaS services are preferred, over IaaS as much as possible to reduce any infra-related maintenance overhead.
* Infra-as-Code (see [Project Roadmap]((#project-roadmap))) is the default way to deploy any Azure resource, we are open to any practical ways but the team mainly adopts Bicep, Terraform and/or PowerShell at times.
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

### Our Branching Strategy

Our team practices GitFlow branching, any feature is a short-lived branch that you can clone from Dev.
Once your great work is done, do a pull request and find any team mate for a second pair of eyes before merging into Dev.
I like to work on bite-size feature, so it is important how the User Stories are broken down into "manageable-sized" Tasks and each Task should only do one thing.  
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

For describing software architecture, we like to use the [C4 Model](https://c4model.com/) modelling technique. I reckon that there are other mature techniques as well like [4+1 View Models](https://en.wikipedia.org/wiki/4%2B1_architectural_view_model) and UML but I favour C4 due to its lean and simple nature for both non-technical and technical folks.  
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

#### Component Diagram - Layered Architecture Diagram
A supplementary Layered architecture diagram is added to explicitly describe the adoption of a Layered architectural style. I feel this diagram can better help developers to visualize the packages used in each Layers and the Cross-Cutting Concerns packages.

<img src="https://user-images.githubusercontent.com/43234101/170198413-068eaedf-93e2-4954-87b5-7f4d03a24226.png"  width="600" height="550" />  

#### Component Diagram - Component Interaction Diagram

* API Controller layer (see [Api Specifications](#api-specifications)) is the entry point to Web App and it contains all package dependencies including cross-cutting (concerns) libraries like Serilog (logging framework). Dependency Injection is configured here to map Interfaces to their relevant concrete classes.  
* With reference to the [12 Factor App](https://12factor.net/) guiding principles, app configuration properties and secrets will always be loaded from environment       variables. And the App Services that are used to host ApiServer, by design, already injects configurations as environemnt variable. 

<img src="https://user-images.githubusercontent.com/43234101/170208325-93a86940-294c-434d-9c44-6b13bc3ac29a.png" width="1100" height="600" />  

<br />
<br />  

## AppSec


### AppSec Hobby Projects  

During this time of learning AppSec, I am inspired to start a couple of hobby projects on fuzzing and authorization, the following projects are what I have in mind at high level: 

* Fuzzie - Fuzzie is a Visual Studio Code Extension that brings Web API fuzzing closest to where developers code, the IDE.  
  Fuzzing can happen right within IDE without having to wait until Fuzzer task runs in DevOps pipeline, this can greatly reduce overall bug fix time.  
  
  For Fuzzie to know the API paths and parameters, I am thinking of VSCode Side Bar GUI to allow developers to specify one or more "Fuzzing Profiles", and each           Fuzzing Profile contains API Urls and/or OpenAPI definition file paths/Urls and other info like authentication methods.
  Fuzzing Profiles ultimately gets save as a Yaml file where the Fuzzie-Core module, which is an independent module can take the Yaml config and run any where, even as   a DevOps Task. VSCode Extension is just a shell to execute Fuzzie-Core module.
  
  As for data, Fuzzie will download fuzz data from an Azure Storage I own so I have full control of the data, however, the data is sourced externally from sources like [Big List Of Naughty Strings](https://github.com/minimaxir/big-list-of-naughty-strings) and [SecList](https://github.com/danielmiessler/SecLists).
  
* CanYou - A policy-based authorization engine that supports a hybrid of Role-Based Access Control and Attribute-Based Access Control design and offers
  * REST APIs as Backend 
  * a Web Frontend for admins to setup and configure role, actions/features and role-feature mappings

  CanYou plans to use [Open Policy Agent (OPA)](https://www.openpolicyagent.org/docs/latest/) as the backend policy engine, and users can express their authorization     policies using a domain-specific language call [Rego](https://www.openpolicyagent.org/docs/latest/policy-language/). The Web Frontend also allows admins to write and   test their Rego policies, policies can include attributes like department, team, reporting manager and more, which is the vital data for Attribute-Based Access         Control.  
  
  CanYou assumes the systems handle authentication on their own, a good authn candidate can be a Token-based authentication where roles and attributes of users are       stored in claims or OIDC-ID tokens. Systems can pass these user attributes to call CanYou API, and Rego policies are executed to make authorization Permit/Deny         decisions.
  Hopefully, with this generic authorization engine, many applications can take advantage of it and not having to build their own, which is commonly the case.

### AppSec Practices in SDLC  

Through my recent AppSec exploration and studies, I have came up with the following sections that describe the AppSec practices I plan to execute for each SDLC phase. These sections are constantly updated as my studies progess.  

| SDLC | AppSec Practices |
|----------|----------|
| Planning | AppSec Training & Awareness |
| Define Requirement | Define Security Requirements |
| Design & Protoyping | Threat Modelling |
| Development | Security Unit Tests, Secure Coding and  Secure Code Review |
| Testing | SAST, Vulnerability Scans, Peneration Tests and  Fuzz Tests |
| Deployment | DevSecOps |


### AppSec Training & Awareness | SDLC - Planning Phase

I devised a study plan for me to stay focus in learning and dive deep into the AppSec areas I believe is important in my AppSec journey.
I hope to learn more secure coding techniques and be experience in spotting vulnerabilities in code through trainings below.
 * OWASP Top 10 current, past and [other](https://portswigger.net/web-security/all-materials) web vulnerabilities
 * Go through [Secure Code Warrior](https://portal.securecodewarrior.com/#/website-trial/web/injection/sql/c_sharp/web_forms) or similar gamified training programs
 * Train using [PicoCTF](https://picoctf.org/) cybersecurity challenges
 * Learn Ethical Hacking and explore Certified Ethical Hacker CEH v11
 * AppSec training
 * Azure Security
 * Get CISSP certification    

### Define Security Requirements | SDLC - Define Requirement Phase

Similar to defining business/functional requirements and non-functional requirements, by explicitly defining and documenting security requirements as User Stories, software delivery team can ensure that security controls can be unambiguously built into the systems, just like any other functional requirements.  
Security requirements can be gather from generally 3 sources in my studies, but most importantly these security requirement gathering sources can be repeatedly applied to future projects.   

 * Derive from business requirements - for example a system has a requirement that needs 2 admins to approve before a transaction can continue.  
   This 2-Admin Approval is a security requirement derived from business requirement.  
   
 * Derive from "applicable" guidline line items from [OWASP Application Security Verification Standard](https://owasp.org/www-project-application-security-verification-standard) -  
   Applicable means to include relevant guidelines for instance guideline item: "File upload - app should not accept large files larger than storage can hold, causing    a denial of service".  
   If the system does not have any requirement on upload files then this guideline item is not applicable.  
   Examples of security requirements from OWASP Application Security Verification Standard:  
   * As a user, I should be able to view and edit my profile. I should not be able to view or edit anyone else's profile
   * As a user, I should be able to signin to the system using my user name and password, with a password complexity of 12 alphanumeric characters with any one             of the word character being capitalized
   * As a developer, I should store all secrets, symmetric and asymmetric public/private keys and certificates in Azure App Configuration or Azure Key Vault. I should not store them anywhere else  
     
 * Turn security requirements into an <ins>Abuse Case</ins> and act like an attacker -    
   * As an attacker, I want to add massive amount of items in shopping cart and checkout without proceeding to payment, so that I can reserve stocks by not buying            anything, causing the system to be in low-stock conditions
   * As an attacker, I want to repeatedly hit the system's API with the same or random inputs and have the API return successful responses, so that I can cause a          Denial of Wallet, when the API is running on a serverless platform

### Threat Modelling | SDLC - Design & Protoyping Phase

Threat modelling is done after I have completed the [Azure architecture diagram](#container-diagram).  
Threat model designer file can be found [here](https://github.com/weixian-zhang/FoodTruckJunkie/blob/main/docs/ThreatModel-FoodTruckJunkie.tm7) and the exported       Report can be found [here](https://github.com/weixian-zhang/FoodTruckJunkie/blob/main/docs/ThreatMode-Report-FoodTruckJunkie%20WebApp.htm).  
  
The detected threats are evaluated one by one, for each threat that is mark as "Needs Investigation",
a work item will be created to further explore mitigations strategies such as:
  * code-based enhancements - for example encode HTML, Javascript output. Validate and whitelist input.
  * configuration changes to existing Azure resource - for example update App Service to not return web server information in response headers.  
    Or tweak Azure Storage settings to not allow Anonymous access and more.
  * Adding new security related Azure services or 3rd-party security COTS products whenever necessary
    * Implementing defence-in-depth principles by introducing layered defence for example: Add a DMZ VNet with NextGen Firewall/Azure Firewall to ingest all Internet         traffic, perform TLS offloading and IDPS, before further routing to Application Gateway WAF for web vulnerabilities scan.
    * And/or implement Host-based protection (a.k.a endpoint protection) to detech VM level vulnerabilities
    * And/or 3rd-party SaaS-based WAF to inspect traffic before routing traffic to web app and more
  * Re-architect - another option could be to re-architect the design to use services that can be deployed in VNet.  
    Example App Service Environment, Integrated Service Environment
  * Adopt industrial security benchmarks to harden Azure environment for example Azure CIS  

### Secure Coding | SDLC - Development Phase

* Secure coding practices - I plan to familiarize with [secure coding practice checklists](https://owasp.org/www-pdf-archive/OWASP_SCP_Quick_Reference_Guide_v2.pdf)     and follow the guidelines as I write software, some examples include:    
  * do not disclose sensitive info in error message
  * close DB connection right after use, leaving  persisted TCP connections are like leaving a back-door for adversaries to exploit
  * validate all inputs
  * encode all outputs including HTML, Javascript, CSS, XML, JSON, Http Headers and more
           
* App secrets
  * if development is using .Net use the [User Secret](https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-6.0&tabs=windows)   feature       from .Net SDK
  * App secrets should be kept in Azure App Configuration, Azure Key Vault or gets injected as Environment Variables by some Azure services
* Pre-commit Git Hook - use Git pre-commit hook like [GitGuardian Shield](https://blog.gitguardian.com/setting-up-a-pre-commit-git-hook-with-gitguardian-shield-to-scan-for-secrets/) to prevent committing secrets accidentally
* Install DevSkim VSCode extension - DevSkim provides inline security assessment while writing codes on the fly, rectify any security assessment prompted by DevSkim
* Unit Testing - Aim for at least 90% of code coverage when writing unit test cases. Unut tests should also cover non-happy flows  

### Security Unit Tests | SDLC - Development Phase

In additional to standard unit tests, security unit tests should also be written to cover security controls developed according to [Security Requirements](#define-security-requirements--sdlc---define-requirement-phase). Security unit tests should try to <ins>bypass</ins> security controls.  
I wrote a simple [Fuzzing Unit Test](https://github.com/weixian-zhang/FoodTruckJunkie/blob/main/tests/unit_tests/FoodTruckJunkie.ApiServer_Security_Tests/ApiServerControllerFuzzTest.cs) to test a Web API operation that takes in 4 parameters. The unit test also extends XUnit framework to load fuzz data from [SecList](https://github.com/danielmiessler/SecLists/blob/master/Fuzzing/6-digits-000000-999999.txt).  
<br />
![image](https://user-images.githubusercontent.com/43234101/171370425-a21ca1a6-8305-4cfb-bde2-3d46a3ed0e34.png)


### Secure Code Review | SDLC - Development Phase

This is an important step to finding security flaws in codes and is an enhancment to standard code reviews by focusing on high risk code modules, company's security standards and industrial security compliances.  
The following is a set of areas for reviews I will try to cover depending on the amount of time spent in review.  
* Prepare for a secure code review by first understanding
  * at high-level how the App works
  * who are the users
  * Who should be able to do What
  * major frameworks and libaries  
  
* Spot vulnerabilities in codes based on OWASP Top 10 Web Vulnerabilities, examples:
  <br />

  A mistake made on purpose to demonstrate data exposure vulnerability, code can found [here](https://github.com/weixian-zhang/FoodTruckJunkie/blob/main/src/FoodTruckJunkie.ApiServer/ErrorHandlingActionFilter.cs).   
  <img src="https://user-images.githubusercontent.com/43234101/171358368-a0228308-7c2d-4676-99b5-67cb7fb489c3.png" width="800px" height = "500px" />  
  <br />
  Examples of vulnerabilities I attempted to find from some random codes online  
  <img src="https://user-images.githubusercontent.com/43234101/170963911-ea337dff-d786-4bed-ad9d-8950429a08c0.png" width="800px" height = "500px" />  
  <br />
  <img src="https://user-images.githubusercontent.com/43234101/170964121-31e62786-ba62-4210-8082-7284d45b312d.png"  width="800px" height = "500px" />

* Referencing [OWASP Code Review Guide](https://owasp.org/www-project-code-review-guide/assets/OWASP_Code_Review_Guide_v2.pdf)  

* Focus on High Risk Code Modules
  * Authentication
  * Password handling
  * Access control or authorization
  * User account management
  * Output encoding exist or not and how is it performed
  * User inputs - does validation exist or not and how is it performed
  * Confidential and PII data used in App and how is it handled
  * Logging - log sensitive user info to log storage for audit
  * How is cryptography performed - in HSM or in App memory
    * any insecure outdated cipher still in use
  * Outdated dependencies
  * Developers' comments - may leak sensitive data and secrets
  * Infrastructure as Code like Terraform and Bicep misconfigurations - for example allowing Anonymous access in Storage, allow Internet connectivity in Inbound and       Outbound rules and etc
  * DevOps Pipeline declaration Yaml files  
 
* Frontends - including Javascript web Frontends, Mobile and Desktop Apps. The main goal to secure code reviewing Frontends is to find out any API Keys and secrets       hard-coded in codes, stored in local storage or App config files  

* Organization's Security Standard and Industrial Compliance - examples
  * Azure Best Practices
    * authenticating with an Azure service such as Storage should always use Managed Identity instead of API Key if the service supports Managed                             Identity
    * Custom Web App should log events to services such as Log Analytics, Table Storage or Data Explorer instead of logging to local disk drive which is not easily           queryable
  * PCI Data Security Standard - check if codes are storing CVV number or Expiry Dates to database  


### DevSecOps | SDLC - Deployment Phase
 
 I plan to introduce security-related pipeline Tasks into our Build and Release pipelines as follows (not limited to),
 * <b>Build Pipeline</b>
    * Credential Scanner - detects credentials and secrets
    * SonarQube Static Code Analysis - code smells, bugs and security vulnerabilities ([rules here](https://docs.sonarqube.org/latest/user-guide/security-rules/))
    * WhiteSource Bolt - detects vulnerabilities in open source components and provides fixes and checks licensing
    * OWasp Dependency Checks - detects publicly disclosed vulnerabilities contained within a project’s dependencies
    * Anchore Image Scanner - For scanning Docker images for vulnerabilities, if any team member decides to containerize some sub-systems
    * Run Unit Tests
    
 * <b>Release Pipeline</b>
    * OWASP ZAP - integrated penetration testing tool to detect web vulnerabilities in our Web APIs
    * Fuzz Test - explore the introduction of Fuzzing with web fuzzers like [FFUS](https://github.com/ffuf/ffuf) or [OneFuzz](https://github.com/microsoft/onefuzz/blob/main/README.md)  



### Authentication
(as part of the Project Roadmap I plan to include)  

Users would be able to sign-in with their Microsoft personal accounts (a.k.a Live account.
Behind the scenes, I will be implementing OpenID Connect authn protocol on Portal and ApiServer.
* SPA authn: Portal will use the OAuth Authorization Code Flow PKCE as it is a Public Client.
* API authn: ApiServer being a Confidential Client will be using Auhorization Code Flow
* API authn-chain : if at that point when Food Truck Junkie has expanded to have other features which are implemented as microservices, and has the requirement to       for microservices to microservices API authentication, we will then be implementing Authorization Code Flow-On-Behalf Flow for API-to-API authentication chain         scenario.
  The concept is when an API (API-A) receive an access token from Portal, API-A uses the access token and with its ClientID/Secret, it exchange for another access       token Token-A. This Token-A will be pass on to API-B for authentication. Within Azure AD we will need to configure "Expose API" to add OAuth scopes for each           microservices. Then followed by configuring authorization in Azure AD - App - "API Permissions", specifiying which API scopes are allowed access to which               microservices.
  
### Authorization 
(as part of the Project Roadmap I plan to include)  
 
Many systems rely on Frontend to authorize (allow and deny) calls to Backend APIs, by hiding UI elements like buttons and menu items.  
Within APIs, many of them today uses OAuth 2.0 to perform authorization.  
OAuth scopes can be expressed quite granularly for example in Azure AD adding a scope *API.Admin.User.Reset*, follow by configuring API Permissions to determine which App has access to the defined scope *API.Admin.User.Reset*.  

In my opinion (purely my opinion), I feel its very difficult to configure an enterpise-scale complex authorizaton policy that comprises of maybe both role-based and attribute-based, and using both mechanisms to determine which user can or not access an API.  
Furthermore, authorization module need to store every feature of the system as *action/operation* (UI element or URL paths) in order to process them with policies to derive the permission, hence, making authorization very intimate to the system, yet holds no domain functional value.  

I personally believe that OAuth can be the "first layer" of authorization, and with OAuth we can get a nice secured Access Token packed with user-specifc claims. 
Claims can contains both roles and other attributes like department, job title and more. These are the perfect data setup to Role-Based and Attribute-Based Access Control, RBAC + ABAC.  

For the "second layer " of authorization, I am planning to implement [CanYou](#appsec-hobby-projects).


### AppSec Guidelines in Azure Software Development
Based on my experience as an Azure practiioner and not limited to:  
* Always use Azure Managed Identity (wherever supported) as the authentication mechanism when accessing Azure services
* All Secrets, Asymmetric and Symmetric keys and x.509 Certifications should store in Azure Key Vault
* Always use Private Endpoints whenever the service supports it
* For Virtual Machines, always enable Azure Automanage
* Always enable *Micosoft Defender for Cloud* whenever the provisioned service is supported
* Application Insights SDK should always be implemented for supported languages, or always enable Application Insights on services that has direct integration with.
  Although Application Insights is a App Performance Monitoring (APM) tool
   * the App Map feature could show if web app is contacting any suspicious external endpoints, detecting malware doing Commmand and Control to form backdoors and          executing data exfiltration
   * Application Insight logs could  further joined with other log types in Azure Sentinel for further investigation. Although I have not seen this in action, I think      this has potential.  
     
<br />
<br />

## Api Server Specifications

ApiServer subsystem is an ASP.NET Core 3.1 web application which uses Json as the default data response format.
* ApiServer adopts a URL Api versioning strategy as we want versioning to be explicit and not subtle like header and content-negotitation strategies.
  QueryString Api versioning strategy tends to clutter the Api's natural querystring parameters
* ApiServer also supports a /heath endpoint in addition to report "is alive" status, this endpoint can extend to return monitoring metrics for monitoring tools like     Prometheus to scrape metrics from.
* Supports OpenAPI to
  * improve external developers' development experience by providing them a formal API contract to develop against
  * to support better integration with external API Gateway products, they can easily import the OpenApi Json format as exposed as ""/swagger/v1/swagger.json

| Verbs | Paths | QueryStrings | Description | Status Code | Data Return | Authn | Rate Limit | CORS |
| ------------- | ------------- | ------------- | ------------- | ------------- |  ------------- |  ------------- |  ------------- |  ------------- |
| GET | /api/<b>1.0</b>/searchfoodtrucks | <sub> latitude={decimal}&longitude={decimal}&distantMiles={int}&noOfResult={int} </sub> | <sub> search food truck info by given {latitude} + {longitude} within {distantMiles} </sub> | <sub>200 - OK, 400 - Bad Request</sub> | <sub>"applicant": {string}, "foodItems": {string}, "latitude": {decimal}, "longitude": {decimal}, "address": {string},   "locationDescription": {string} </sub> | <sub>Project Roadmap: OAUTH Authorization Code Flow</sub> | 70 calls/min | <sub>AllowedHeaders:*, AllowedMethods: GET, AllowedOrigins: https://localhost:5001, https://webapp-foodtruckjunkie-api.azurewebsites.net, MaxAgeSeconds: 3000 </sub> |
| GET | /health/1.0 |  | <sub>health status and monitoring information</sub> | 200 | |  <sub>Project Roadmap: OAUTH Authorization Code Flow</sub> | 70 calls/min | <sub>AllowedHeaders:*, AllowedMethods: GET, AllowedOrigins: https://localhost:5001, https://webapp-foodtruckjunkie-api.azurewebsites.net, MaxAgeSeconds: 3000 </sub> |
| GET | / |  | <sub>OpenAPI/Swagger Web UI</sub> | 200 | | <sub>Project Roadmap: OAUTH Authorization Code Flow</sub> | 70 calls/min | <sub>AllowedHeaders:*, AllowedMethods: GET, AllowedOrigins: https://localhost:5001, https://webapp-foodtruckjunkie-api.azurewebsites.net, MaxAgeSeconds: 3000 </sub> |
| GET | /swagger/v1/swagger.json | | <sub> OpenAPI/Swagger Json | 200 </sub> | | <sub>Project Roadmap: OAUTH Authorization Code Flow</sub> | 70 calls/min | <sub>AllowedHeaders:*, AllowedMethods: GET, AllowedOrigins: https://localhost:5001, https://webapp-foodtruckjunkie-api.azurewebsites.net, MaxAgeSeconds: 3000 </sub> |
<br />

### Preventing OWASP Improper Asset Management

* All APIs including previous versions will be clearly documented including Authn mechanisms, Rate Limit and CORS Policy. This is to prevent any old API versions get     left out and attacked without anyone knowing.  
  Ensures everyone knows the "attack surface area".

* API Retirement Plan - create a plan to safely and "quickly" decommision old API versions
  * Monitor API Gateways to get a sense of traffic volume still going to old API
  * Pick a date, could be 90 days, to 6 months or to a year
  * Inform API consumers of the deprecation
  * Monitor old API on API Gateway and reach out to each consumer to find out reason for late migration and support them

* Assets can include Infra components, do discourage sharing the same Infra components like Firewall, Windows AD, DNS server and more across Prod and Non-Prod           environments.  
  When sharing these components, any mistakes in security misconfigurations can cause Production Firewall routing rules to route production traffic to DevTest           environments. Or a DevTest domain name add as A-record to Production Private IP address.
  
* In Food Truck Junkie API versioning implementation, the API versions are mapped to methods as shown below. With a new version API-2.0, it is just another method       within FoodTruckPermitController class, and not a separately deployed API system running on a different runtime. With this, old API versions enjoy the same             protection as the current API like WAF scans, Rate Limiting, Custom Rules, Authn and etc. Hence, not leaving behind any old API versions vulnerable and unprotected.

  <img src="https://user-images.githubusercontent.com/43234101/171579530-62fd1a3d-7e60-4bfd-8517-32f1021b2c9c.png" width="900" height="600" /> 


<br />
<br />

## Database Specifications

I am using Azure Database for MySQL and all setup scripts can be found [here](https://github.com/weixian-zhang/FoodTruckJunkie/tree/main/src/Scripts/DB).  
All scripts are written and tested using MySQL Workbench 8.0.  
Scripts include:
* data loading script that reads food truck data from CSV file and inserts them to the a Table
* table creation script
* stored procedure that uses the Haversine formula to calculate the distant between filtered food truck coordinates, against user's coordinates passed in as paramaters   from web portal.

<br />
<br />  

## Logging & Monitoring  

* All error events are log to MySQL and the reason is that I can easily query/full-text search for errors including date/time range, as compared to the very             inefficient way of logging to text files and having to eyeball and "Ctrl-F" to find errors

* Application Insights is also enabled on the App Service hosting the API for App Performance Monitoring
  As an item for Project Roadmap, I can also include Application Insights SDK to enable tracking of dependencies to MySQL, Storage and even messaging sinks like         Service Bus and Event Hubs in future.

<br />
<br />

## Testings

The current available tests are Unit Tests written using XUnit framework, unit tests covers all 3 layers including API Controller, Service and Repository.
Additionally, Moq library is used to Mock an Interface IDbConnection which represents a connection object to MySQL DB, by mocking we are excluding actual connection to MySQL, as integration test is not part of unit testing.
Mocking can happen due to the consistent usage of Interfaces and all concrete classes must implement Interfaces.
Dependencies are always constructor injected and codes need to refactor to support unit testing if needed.

In our project roadmap, I plan to setup the following types of test:
* Javascript Unit Tests - use [Jest](https://jestjs.io/docs/getting-started) framework to write unit tests for React web app
* Automated Smoke Tests
  * Portal: using [Cypress](https://docs.cypress.io/guides/overview/why-cypress#Setting-up-tests), [Selenium](https://www.selenium.dev/documentation/) or similar Web       UI testing tool to automate UI testing. Smoke test still follows a set of test cases consisting of test steps, however, Smoke Test aims to test if a build is     stable and covers breadth more than depth.
  * ApiServer: using [Artillery](https://www.artillery.io/docs/guides/getting-started/core-concepts#test-definitions) to smoke test ApiServer by pre-configuring the       querystring parameters following a set of defined test cases.
* Functional Tests - functional tests can also be automated using Selenium, Cypress or other similar tool. Functional test follows a set of wel-defined and               comprehensive test cases to go deep into testing functionalities, as compared to Smoke Test which aims for coverage to discover instability in new features.
* Integration Tests - This test focus on cross app domain/network component testing. It can be manual or automated at the GUI layer to trigger calls to MySQL database.
  If in future there are integrations with external systems, testing becomes more tricky as each dev team may need to wait for each other's integration endpoint to be ready before integration tests can occur.  
  In this case we can explore using [Pact](https://docs.pact.io/), a consumer-driven integration test tool that mocks external endpoints by specifying a "contract".
  A contract makes up of Endpoint Url, input parameters and data types, and return data and data type. So as long as both parties follows the Api interface contract,     testing against a Mocked API that adheres to the contract is the same as testing against the real external API.
* Load Tests - [Artillery](https://www.artillery.io/docs/guides/getting-started/core-concepts#test-definitions) can also be used for load testing by increasing the       number of "virtual-users"
* DAST: Penetration Tests - as described in [DevSecOps](#devsecops--sdlc---deployment-phase) Release Pipeline contains OWASP ZAP Scanner to perform black-box testing     on Web API
* Fuzz Test - As mentioned, we could explore Grammer-based Fuzzing to generate templatized inputs to Web APIs, to find bugs and security vulnerability.

<br />
<br />

## Development Challenges
* A challenge with MySQL LIMIT clause is that LIMIT cannot be used with a variable for e.g LIMIT @numberOfResult as this syntax is considered invalid.
  A workaround could be to wrap the whole SELECT statement in string and use PREPARE stmt
  <img src="https://user-images.githubusercontent.com/43234101/169995446-3424ed5e-41b0-439a-9848-74df786660d3.png" width="700" height="250" />  
  An easier way I did to solve this is by just setting "numberOfResult" in ApiServer before passing parameter into MySQL stored procedure.  
  
* I spent several hours on data loading and Latitude and Longitude data type conversion to MySQL with decimal typed columns.  
  Initially I was using MySQL Workbench for data loading, and found out that this feature  is infamously slow: 5-6 mins/per record.  
  I then resort to other tools like *Toad for MySQL* and *HeidiSQL*, turns out that the Latitude and Longitude data are incorrect in MySQL.  
  For example: Lat=36.54323222, in DB=99.00000000.  
  I am about to write a custom tool to do data loading, however I did a last attempt to use MySQL script to do data loading.  
  I also changed Lat/Long column type to *text* instead of decmial, and I have to do text-to-decmial conversion in stored proc for the Haversine calculation.
  My final attempt resolves the data loading (in few secs) and Lat/Long data type incorrect issues, saved me time from writing a custom app.
  
* Frontend - when binding GoogleMap javascript object's "center" property, to input textboxes of latitude and longitude over React-State,
  any text change causes map to grey-out.
  Finally found out that this bug is due to input textboxes are of string type while GoogleMap's "center" property accepts decimal only, which makes perfect sense       since they are coordinates.  
  This is solved by parsing string to float e.g: parseFloat(this.state.latitude)

<br />
<br />

## Project Roadmap - If I Have More Time

* <b>Software Patterns & Practices</b>  
  (Some of the points has be explained in detail in the above sections)  
  
  * Backend For Frontend pattern - in future if more client types like Desktop, mobile and Cli are added, we could explore Backend-For-Frontend (BFF) pattern.
    How BFF works is that there will be a API layer where data and business logic are customized for each Frontend type.
    These BFF client-specific APIs are usually coarse-grain APIs that could be calling other microservices to aggregate data before returning to the client.
    For example, Mobile App may not have an admin module while Desktop and Web clients have it. Hence Mobile BFF API will not contain admin modules as well.
    
  * Adopts the Microservices architectural style as system grows. This will be a brown-field migration to Microservices architecture.
    Each Bounded Context of the system domain can be an independent microservice maintained by a separate team with their own programming language and tech-stack of       their choice
  
  * Setup DevOps Build and Release Pipelines with security Pipeline Tasks
  * Focus on Testing - Automated Smoke Test, Functional Test, Integration Test, Load Test and Fuzz Test
    
* <b>Technology</b>  
  (Some of the points has be explained in detail in the above section)  
  
  * a New Cli App written using Python. and perform authentication using OAuth Device Code Flow.
  * Authentication with Azure AD OIDC
    * Portal with Authorization Code PKCE
    * ApiServer with Authorization Code  
    
  * Authorization
    * with OAuth 2.0 as the "first layer" of authorization
    * for "second layer", explore to start a policy-based authorization engine as a personal project call CanYou  
    
  * Microservices are deployed on Azure Kubernetes private cluster. Service Mesh like Istio can be introduce for API Gateway capability, mutual-TLS authn, 
    and network policy built-in to constraint network reacbility amongst microservices, and more.
  * A data loading daemon app implemented using Azure Function to automatically and timely pull Food Truck permit data file and refresh static data in DB Table
  * Use Azure Static Web App to host Portal instead of App Service so as to take advantage of built-in CDN and DevOps deployment features.
  * Cache search result in Azure Redis using Cache-Aside pattern to return food truck results from previous queries
  * Develope Azure Bicep scripts to setup the Azure environment
  * Azure FrontDoor WAF added in front of ApiServer for better security coverage:
    * Custom Rule to perform rate limiting
    * OWASP TOp 10 web vulnerability scanning on TLS terminated traffic, before routing to ApiServer

* <b>Functional</b>  
  * Search by Address - search nearby food trucks by Address in addition to coordinates  
  
  * Search by Food Type
    * Food type data can be isolated, compiled and save into a dedicated DB Table
    * In the Frontend, food type data can be retrieved when Frontend loads, food type can be cached in browser's local storage
    * Lastly, an auto-complete textbox is added and binds to the local storage cached food type data.  A
      As user types in auto-complete box, food choices are actively prompted  
      
  * Use Azure Boards to track user stories and work items
 
<br />
<br />

## What I Have Learned

The idea of this challenge is an interesting one, most importantly this challenge takes the challenger through a full Software Delivery Lifecycle much like a real world's. Starting from understanding the requriements, to software architecture design, system Infra/Networking architecture design, threat modelling, software module-level design (package-level), software development in full-stack involving polyglot languages and frameworks from Frontend, Backend to Database, to deploying system to Azure (in this case), and finally to documentation.
In a real-world software project, we have many other participating roles like Business Analysts for requirements gathering and documentation, Software Engineers, Software Architects, Software Tech Leads, System Admins to OS/Infra/Networking setup and configuration, DBA to setup and configure DB clusters (and even run our TSQL scripts some times). Test specialists using specalized software to run penetration tests against our systems.
3rd-party security auditors to scrutinize our OS hardening configurations, firewall rules, network routing policies, web server configurations and more.
And not forgetting Project Manager, Scrum Master, Subject-Matter Experts and Product Specialists of certain line-of-products.

Having said a mouthful, this challenge although a one-person size project, it can put one through most of the SDLC stages and playing different roles all at once, simulating a real-world project. I really enjoyed myself.

* Before I start this challenge, I took several hours to learn about Latitude, Longitude, the geographical East-West reference line of Greenwich Meridian,
  and how to plot coordinates on a world map.
  This may be common sense knowledge to other people but for me, I just didn't manage to learn before this challenge. I am glad I did now.
* I always wanted to learn how to embed maps into an App and have the icons on the map move by itself. The idea of tracking someone or device on a map watching it move   is really fun. Finally got a purposeful geospatial requirement to try Map technology. I wanted to use Azure Map at first but due to there are many open-source         libraries on integrating Google Maps into a React web app, and most importantly in the essence of time, I chose Google Map.
* I also chose MySQL because I have not work on MySQL before. Thanks to that I am able to discover a very common usage of LIMIT clause but yet with big limitation in     my opinion. (see [Development Challenges](#development-challenges)).
