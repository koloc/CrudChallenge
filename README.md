# CrudChallenge

For the challenge I designed a long term view of the system. The following assumptions where made:
* The reporting will be more intense eventually (weekly, monthly, on demand...)
  * That's why I chose to divide the responsibilities in terms of services and DBs (CQRS or similar pattern)  
* The notifications will be decoupled from the system and will be used by the reporting service as well to let users know when the report is ready
  * Users will be able to download reports from the email (S3 link)
* The forecasting service will be decoupled from our systems by sending events to a message broker (queue)
  * Using websockets implies that we have to maitain an open connection at all times
  * Using REST might work but we are overloading our main system

## Diagram

![CrudChallenge](https://github.com/koloc/CrudChallenge/assets/8647868/d2920ff1-4822-45b6-86ce-c550b38bf376)

## Code

The code won't be an exact representation of the design specially to keep it simple (and for time reasons).

Main differences:
* The solution has been designed as a simpler CRUD with layers approach
  * We have an API layer and a Data layer.
    * There's no business layer because there is no actual business logic but be that the case then the API layer would get the business layer injected and the business layer would get the data layer.
  * There's a Model project for keeping DTOs, etc...
* Versioning has been implemented by using a querystring (?api-version=1.0) to keep it simple but there are better ways to do it
  * This solution means that we have to duplicate controllers and if we only have a different version of one of the four methods of the controller then the other three will have to be duplicated. That's not ideal.
  * With a bit more time I would've implemented it by versioning the methods inside the same controller (can be accomplished with the same library)
  * Also instead of using a querystring a better approach would be to use /v1/ or /v2/ as part of the URI (also can be implemented with the same library)
* For exception handling I implemented a simple middleware that translates ApplicationExceptions to BadRequests and Exceptions to InternalServerErrors.
* For logging I used Serilog and since it was requested to log exceptions in different files (not a fan of this to be honest) I ended up doing the following:
  * Logging Info/Debug in one file
  * Logging Error/Fatal/Warning in another file
* Created a Notifications project with an Email service
  * I would've wanted to configure an actual emailing service (Mailkit for example) but do to time constraints wasn't able to.
  * Notice that there are no queues in the solution. Simple and direct calls for simplicity.
* Created a Worker project for reporting
  * The only thing that it does is count the number of products on the DB and "sends" an email (actually ends up logging the report result)  
