# Web API Stack

## Overview
* Routing
* Controller Selection
* Dependency Management
* Persistence
* Type Mapping
* Diagnostic Tracing
* Error Handling

## The ASP.NET Web API Pipeline

Caller makes a web request using a versioned URL

##### Pipeline starts activation of the appropriate controller
* based on **routes** registered at application start-up
* using custom **controller selector**


##### Pipeline creates an instance of the selected controller
* using **NinjectDependencyResolver** to satisfy all dependencies


##### If ISession is required
* NinjectDependencyResolver will call **NinjectConfigurator.CreateSession()**
* a **new instance** is created, and a session with the database is opened
* this is **bound to the web context** so that it can be resued for subsequent dependencies


##### Pipeline calls the custom unit of work attribute OnActionExecuting
* this begins a new **database transaction**


##### Pipeline invokes the correct controller action method
* this executes the actual **business logic**


##### Pipeline calls the custom unit of work attribute OnActionExecuted
* this either **commits or rolls back** the database transaction
* this **closes and disposes** of the ISession instance
