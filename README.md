# Beyond Earth Application

## Directory structure
* doc: documents relating to the code base (requirements, installation, guides)
* lib: third-party libraries and packages used by the application
* src: source code (including the solution file and project folders)

## Projects
|Name|Type|Purpose|
|----|----|-------|
|BeyondEarthApp.Common |Class Library |Contains framework functionality. (Nothing specific to the API or the database.)|
|BeyondEarthApp.Data |Class Library |Contains the **Domain Model** (the POCOs that will be used by the ORM to persist data). Contains the **Data Access** Interfaces (but nothing specific to SQL Server).|
|BeyondEarthApp.Data.SqlServer |Class Library |Contains the **Data Access** Implementations (SQL Server specific). Contains the **ORM Mapping**.|
|BeyondEarthApp.Web.Api |ASP.NET Web Application - empty template with references for Web API |Contains the REST **Service Application** (this is hosted by IIS at runtime). Contains all controller, routes, handlers, connection strings, etc.|
|BeyondEarthApp.Web.Api.Models |Class Library |Contains the REST API **Resources Model** (the 'types').|
|BeyondEarthApp.Web.Common |Class Library |Contains functionality common to web and service applications.|
|BeyondEarthAppDb |SQL Server Database Project |Contains the SQL Server **Database** (schema, code, data, etc). The output of this project is to publish the database to the specified target (new or existing database).|