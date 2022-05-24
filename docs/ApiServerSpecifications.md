## Api Server Specifications

| Verbs | API Paths | QueryStrings | Description | HTTP Status Code
| ------------- | ------------- | ------------- | ------------- | ------------- |
| GET | /api/searchfoodtrucks | latitude={decimal}&longitude={decimal}&distantMiles={int}&noOfResult={int} | search food truck info by given {latitude} + {longitude} within {distantMiles} | 200, 400 |
| GET | /health | none | health status and monitoring information | 200 |
| GET | / | none | OpenAPI/Swagger UI | 200 || GET | / | none | OpenAPI/Swagger UI | 200 |
| GET | /swagger/v1/swagger.json | none | OpenAPI/Swagger Json | 200 |

