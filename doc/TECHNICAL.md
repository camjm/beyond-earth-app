# Technical Structure

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

## Architecture Components
Tools to help implement the web service after the high-level design.

#### Data Access & Object Persistence
Uses **NHibernate** as the Object Relational Mapper (ORM), which supports the Unit of Work patterion (*ISession*).

#### Type Mapper
Uses **AutoMapper** to map the data between objects (the REST resource types and the persistent domain data model).

#### IoC Container
Uses the **Ninject** container to manage dependencies by implementing ASP.NET *IDependencyResolver*.

#### Logger
Uses the **log4net** framework. Loggers can be used with IoC containers.

#### Testing Framework
Uses the **NUnit** framework because it is simple, has a full-featured *Assert* class, and fluent interface.
Uses **Moq** for the test mocking framework.
