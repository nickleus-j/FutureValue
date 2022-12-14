# FutureValue
Practice App with asp.net core
Need the following
* Dot net 6 sdk and runtime
* A sql server instance
* Recommend Visual studio 2022 since the steps here are from Visual Studio
* Git for cloning the repository(recommend Main branch for initial build)
After installing Visual Studio 2022 and Setting up Sql server instance, the solution should available for debugging and tinkering. The first csproj to build is the web API. The MVC(FutureValue.Web) and Angular(FutureValue.Angular) must be built after API.
## Start with the API project
The presentation projects both depend on the API project. The API has to be running for the presentation project to show data. Alternatively, the API project can be deployed somewhere and the presentation projects can just reference the URL of the API.
## Environment variables
Certain Environment variables can be used to configure the API project. User secrets and appsettings.json can also contain these configuration values The variables are the following
* FvDb: Connection string to the database
* AllowedOrigins: hosts of origins allowed for CORS
## Build and Debug API project (FutureValue.WebApi)
Connection string will only be needed to be configured on API project(FutureValue.WebApi)
Use user secrets to add connection strings.
```
{
  "ConnectionStrings": {
    "defaultDb": "Data Source=.;Initial Catalog=FutureValue;Integrated Security=true"
  }
}
```
The data source can be different in case sql server is remote.
Upon first start of the API, data will be seeded to the connected database.
A Swagger page will appear on a browser. After seeing the Swagger page, the seeded database should be visible to sql server manager if it's connected to that database server.
The URLs on the swagger page should produce a json when browsing them.
API is considered built.
## MVC Presentation (FutureValue.Web)
Start the API without debugging first. Take note of the base URL of the API after seeing the Swagger page. 
Appsettings.json has an attribute called ApiBaseUrl where the URL of the API to call is to be set.
```
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "ApiBaseUrl": "https://localhost:7257/",
  "AllowedHosts": "*"
}
```
After verifying the config, the project can be built and debug in VS. The browser should show a table of Projection forms. The seeded data should be seen at the first run.
That ends building the MVC project.
## Angular Presentation Project (FutureValue.Angular)
Start the API without debugging first. Take note of the base URL of the API after seeing the Swagger page. 
The URL in the browser should be the same as the one found in 
[root]\FutureValue\FutureValue\FutureValue.Angular\ClientAppsrc\app\futurevalues\AppSettings.ts 
```
export class AppSettings{
    AppUrl:string="https://localhost:7257/";
}
```
Just change the URL value if needed.
Afterwards, the Angular can be debugged and built. It might take a while to restore and install npm packages. Be aware of the risk of timeout while npm install is running. There is an initial loading page before the actual Angular site is shown. When the Angular site starts, the browser should show a table of Projection forms. The seeded data should be seen at the first run.
That ends building the Angular project.
## More notes on the Presentation projects
They do not reference the other dlls/csproj. What is important for these projects to show data is the right base URL address. The intention of not referencing the dlls is to attempt to present all the data via API calls.
## About the Test projects
* The test projects are made with xUnit. 
* The Domain has a test project to test the projections of future values. 
* The EfImplementation uses in memory database to generate test data.
* Selenium web driver is used in the MVC and Angular presentation projects to check the contents of the site generated.
