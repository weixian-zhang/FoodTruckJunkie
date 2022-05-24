## Api Server Specifications

ApiServer subsystem is an ASP.NET Core 3.1 web application which uses Json as the default data response format.
* ApiServer adopts a URL Api versioning strategy as I wanted versioning to be explicit and not subtle like header and content-negotitation strategies.
  QueryString Api versioning strategt tends to clutter the Api's natural querystring parameters
* ApiServer also supports a /heath endpoint in addition to report "is alive" status, this endpoint can extend to return monitoring metrics modern monitoring tools like Prometheus to scrape metrics from.
* Supports OpenAPI to
  * improve external developers' experience
  * API Gateway products can easily import ApiServer's API spec using OpenApi Json format as exposed as ""/swagger/v1/swagger.json

| Verbs | API Paths | QueryStrings | Description | HTTP Status Code
| ------------- | ------------- | ------------- | ------------- | ------------- |
| GET | /api/<b>1.0</b>/searchfoodtrucks | latitude={decimal}&longitude={decimal}&distantMiles={int}&noOfResult={int} | search food truck info by given {latitude} + {longitude} within {distantMiles} | 200, 400 |
| GET | /health | none | health status and monitoring information | 200 |
| GET | / | none | OpenAPI/Swagger UI | 200 || GET | / | none | OpenAPI/Swagger UI | 200 |
| GET | /swagger/v1/swagger.json | none | OpenAPI/Swagger Json | 200 |

