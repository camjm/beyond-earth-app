# REST API Service

## API Service Design

### Game Operations
|URI   										                |Verb 		|Description																				                                        |Role      |Status     |
|---------------------------------------- |---------|-------------------------------------------------------------------------------------------|----------|-----------|
|/api/games                               |GET      |                                                                                           |User      |Done       |
|/api/games/123                           |GET      |                                                                                           |User      |Done       |
|/api/games/123                           |PUT/PATCH|                                                                                           |User      |Done       |
|/api/games                               |POST     |                                                                                           |User      |Done       |
|/api/games/123/activations               |POST     |                                                                                           |User      |Done       |
|/api/games/123/completions               |POST     |                                                                                           |User      |Done       |
|/api/games/123/reactivations             |POST     |                                                                                           |User      |Done       |
|/api/games/123/technologies              |PUT      |                                                                                           |User      |Done       |
|/api/games/123/technologies              |DELETE   |                                                                                           |User      |Done       |
|/api/games/123/technologies/456          |PUT      |                                                                                           |User      |Done       |
|/api/games/123/technologies/456          |DELETE   |                                                                                           |User      |Done       |

### Affinity Operations
|URI   										                |Verb 		|Description																				                                        |Role      |Status     |
|---------------------------------------- |---------|-------------------------------------------------------------------------------------------|----------|-----------|
|/api/affinities                          |GET      |                                                                                           |User      |TO DO      |
|/api/affinities/123                      |GET      |                                                                                           |User      |TO DO      |
|/api/affinities/123/bonuses              |GET      |                                                                                           |User      |Not Sure   |
|/api/affinities/123/technologies         |GET      |                                                                                           |User      |Not Sure   |

### Status Operations
|URI   										                |Verb 		|Description																				                                        |Role      |Status     |
|---------------------------------------- |---------|-------------------------------------------------------------------------------------------|----------|-----------|
|/api/statuses                            |GET      |                                                                                           |User      |TO DO      |

### Technology Operations
|URI   										                |Verb 		|Description																				                                        |Role      |Status     |
|---------------------------------------- |---------|-------------------------------------------------------------------------------------------|----------|-----------|
|/api/technologies							          |GET		  |Gets the full list of technologies. Filter by URL query string arguments.					        | | |
|/api/technologies						         	  |POST		  |Creates a new technology. Returns the new technology.										                  | | |
|/api/technologies/123						        |GET		  |Gets the details for the specified technology.												                      | | |
|/api/technologies/123						        |PUT		  |Updates the specified technology. Returns the updated technology.							            | | |
|/api/technologies/123/buildings			    |GET 		  |Gets the buildings of the specified technology.											                      | | |
|/api/technologies/123/buildings			    |PUT		  |Replaces all buildings of the specified technology. Returns the updated technology.		    | | |
|/api/technoligies/123/buildings			    |DELETE		|Deletes all buildings from the specified technology. Returns the updated technology.		    | | |
|/api/technoligies/123/buildings/456		  |PUT		  |Adds the specified building to the technology. Returns the updated technology.				      | | |
|/api/technoligies/123/buildings/456		  |DELETE		|Deletes the specified building from the technology. Returns the updated technology.		    | | |
|/api/technologies/123/units				      |GET 		  |Gets the units of the specified technology.												                        | | |
|/api/technologies/123/units				      |PUT		  |Replaces all units of the specified technology. Returns the updated technology.			      | | |
|/api/technoligies/123/units				      |DELETE		|Deletes all units from the specified technology. Returns the updated technology.			      | | |
|/api/technoligies/123/units/456			    |PUT		  |Adds the specified units to the technology. Returns the updated technology.				        | | |
|/api/technoligies/123/units/456			    |DELETE		|Deletes the specified units from the technology. Returns the updated technology.			      | | |

### Building Operations
|URI 										                  |Verb     |Description																				                                        |Role      |Status     |
|-----------------------------------------|---------|-------------------------------------------------------------------------------------------|----------|-----------|
|/api/buildings								            |GET	  	|Gets the full list of buildings. Filter by URL query string arguments.						          |User      |To Do      |
|/api/buildings/123							          |GET		  |Gets the details for the specified building.												                        |User      |To Do      |
|/api/buildings								            |POST	   	|Creates a new building. Returns the new building.											                    |Admin     |To Do      |
|/api/buildings								            |DELETE  	|                                                  											                    |Admin     |To Do      |
|/api/buildings/123							          |PUT		  |Updates the specified building. Returns the updated building.								              |Admin     |To Do      |
|/api/buildings/123							          |DELETE	  |                                                             								              |Admin     |To Do      |

### Unit Operations
|URI 										                  |Verb 		|Description																				                                        |Role      |Status     |
|-----------------------------------------|---------|-------------------------------------------------------------------------------------------|----------|-----------|
|/api/units									              |GET  		|Gets the full list of units. Filter by URL query string arguments.							            |User      |To Do      |
|/api/units/123								            |GET		  |Gets the details for the specified unit.													                          |User      |To Do      |
|/api/units             									|POST		  |Creates a new unit. Returns the new unit.													                        |Admin     |To Do      |
|/api/units             									|DELETE	  |                                                      							                        |Admin     |To Do      |
|/api/units/123           								|PUT  		|Updates the specified unit. Returns the updated unit.										                  |Admin     |To Do      |
|/api/units/123           								|DELETE  	|                                                     										                  |Admin     |To Do      |


## REST Architecture
REST APIs are resource-centric. They are oriented around nouns, and the actions are constrained by HTTP Verbs.
* Unique URI for each resource.
* Consistent use of HTTP Verbs and Status Codes.
* Self-describing: no foreknowledge required to traverse the domain and state graph.

### HTTP Status Codes
Uses semantic HTTP codes.

### HTTP Verbs

#### GET
Idempontent and safe (no changes to system). Get the resouce or collection of resources.

#### PUT
Idempontent. Replace (or create) the resouce or collection of resources - identifier specified by client.

#### POST
Create new resource - identifier generated by the service.

#### DELETE
Idempontent. Delete the resource or collection of resources. Can also be used for delete-like operations such as close, inactivate, and hide.

### Hypermedia as the Engine of State
Contractless application: actions and operations are discoverable with hypermedia. Clients navigate through the application state by the hypermedia provided by the service. Responses to requests for a resource include URIs (and other information to compose requests) to other resources, and actions available for the requested resource.
```xml
<Tasks>
  <TaskInfo Id="123" Status="Active">
    <link rel="self" href="/api/tasks/123" method="GET" />
    <link rel="users" href="/api/tasks/123/users" method="GET" />
    <link rel="history" href="/api/tasks/123/history" method="GET" />
    <link rel="complete" href="/api/tasks/123" method="DELETE" />
    <link rel="update" href="/api/tasks/123" method="PUT" />
  </TaskInfo>
</Tasks>
```

### Metadata
Collection+JSON or HAL are standards taht can be used to describe APIs.
