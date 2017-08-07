The provided sample code is a small skeleton C# Web API application.
It can be opened and run in Visual Studio 2015.
The application manages users and their tasks.

The sample demonstrates a lot of concepts that I consider as "good design practices".

Some key features:
- follows SOLID principles
- embraces DDD concepts
- embraces TDD concepts
- REST API
- low coupling
- uses dependency injection
- code easy to test and mock
- used design patterns: factory, singleton, adapter, composite, decorator, flyweight, template
- DAL is fully decoupled and can easily be replaced (a simple in-memory database is used, but it can easily be replaced with any ORM/NoSql/Object DB, or a combination of all of them)
- domain objects are mapped to database objects, and vise-versa, in DAL (data structures can differ)
- domain objects are mapped to DTO objects, and vise-versa, in services (data structures can differ)
- "Request-Response" pattern in services
- "Common" components are generic, domain agnostic and can be reused in multiple projects
