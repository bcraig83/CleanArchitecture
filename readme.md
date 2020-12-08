# Clean Architecture Solution Template

![.NET Core](https://github.com/bcraig83/CleanArchitecture/workflows/.NET%20Core/badge.svg)

This repo is based on [this](https://github.com/jasontaylordev/CleanArchitecture), with some tweaks.

## How to use this repository

The master branch contains a sample application built in the style of Clean Architecture. Use this as a reference to build out your own projects.

If you want a fresh starting point:

- Download a tagged "StartHere" release as a zip file (e.g. https://github.com/bcraig83/CleanArchitecture/releases/tag/v1.2.0-StartHere)
- Unzip the file.
- Rename the solution to your own solution name.
- Write some code!

The latest "StartHere" branch can be found under "StartHere/latest", but you should be able to find all previous versions under the same branch directory (for example, v1.1.0 is found under "StartHere/v1.1.0").

## What's tweaked

### General

- All projects have been upgraded to use dot net core 3.1.
- In general, I've migrated all dependencies to the latest-and-greatest, unless doing so included breaking changes.
- Implemented an eventing model, which allows us to created events and handlers. These can be triggered when an entity is written back to the database. So in other words, push notifications of changes to the state in the persistance layer.

### Domain

- Added a solution directory for events. There's a little bleed here with [this repository](https://github.com/ardalis/CleanArchitecture).
- Have (controversially) added some repository interfaces. The concept of repositories is purely to allow decoupling of database from application when writing functional tests. I don't think the concept itself is controversial, but where to place these interfaces might be. It could be argued they belong in the application layer. I've gone with domain, purely because so many enterprise applications have their domain stored in the database. 
- Added some domain-level unit tests.
- Some very minor code tidying.

### Application

- Upgraded dependencies to the latest and greatest, with the exception of FluentValidation, because the latest version of that is a major version upgrade, so it includes breaking changes that would need to be rewritten here.
- Added some additional unit tests to the unit test project, just to show how you might unit test some of the individual classes within the Application layer.
- Some minor personal preferences :)
    - Inside the commands/queries, I've boxed off model classes in a separate directory.
    - I have created a separate class for the command/query handlers. It's still in the same directory, so I think it still fulfills the concept of having everything related together. I just don't like two classes in one class file :)
    - I've created a directory for EventHandlers. These will be invoked when domain events are fired. See `TodoItemCompletedEventEmailHandler` for a nice example.
- Some very minor code tidying, such as failing fast out of a method when we know a failure condition has been met.
- I've added application level functional tests.
    - Crucially, these tests DO NOT use a database. Instead, I'm currently using a stubbed version, under the "Fakes" directory. There's an agrument here to just using a mocking library like Moq, which I'm finding increasingly hard to ignore...

### Infrastructure

- Further separated this level into DataAccess and Integration. It just feels like data access and persistance is such a big portion of most applications, it needs to have it's own separate layer.
- Upgraded dependencies to the latest and greatest.
- I have to admit a lack of understanding of EF Core. I think I may have made a mess of the persistance layer. Specifically,  the "InitialCreate" class. Let's call it a work-in-progress for now! 

### Presentation

- I've made this layer a directory. The rationale is, that we may went to present the same app in a variety of formats. Maybe we will have an application that we can hit via a REST API, that project can go in here, but call down into the Application layer. But then we might need the same functionality via gRPC. Perhaps we need a command line version of the App, or a Blazor application, or a desktop application. All these 'runtime' apps should live here, and have no business logic, only 'protocol' logic, if required.
- I've started implementing a REST API to prove it out. Will add more over time.

### Tests

- Split the tests by type.
    - `exploratory`
        - This might be little experiments, maybe a small-scale console app for testing out an idea, basically a sandbox.
        - I would create these as required, and keep them as long as they are useful.
        - If the test is implemented later in some better, more efficient way, I would pull it out of here.
    - `integration`
        - These specifically DO test the infrastructure layer.
        - I'm using an in-memory database for these tests.
    - `unit`
        - Pretty much standard unit tests.
        - Use mocking library (moq) but avoid using it for verification.

