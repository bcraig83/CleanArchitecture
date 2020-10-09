# Clean Architecture Solution Template

![.NET Core](https://github.com/bcraig83/CleanArchitecture/workflows/.NET%20Core/badge.svg)

TODO: this readme is a work-in-progress, mainly because the code base is also a work-in-progress! Note to self: make sure readme and code base are in sync by the time I'm "done" with this repo.

This repo is based on [this](https://github.com/jasontaylordev/CleanArchitecture).

## What's tweaked

### General

- All projects have been upgraded to use dot net core 3.1.
- In general, I've migrated all dependencies to the latest-and-greatest, unless doing so included breaking changes.
- Included an eventing model, which allows us to created events and handlers. These can be triggered when an entity is written back to the database. So in other words, push notifications of changes to the state in the persistance layer.

### Domain

- Added a solution directory for events, but have not added any examples yet. There's a little bleed with [this repository](https://github.com/ardalis/CleanArchitecture).
- Some very minor code tidying.

### Application

- Added some additional unit tests to the unit test project, just to show how you might unit test some of the individual classes within the Application layer.
- Upgraded dependencies to the latest and greatest, with the exception of FluentValidation, because the latest version of that is a major version upgrade, so it includes breaking changes that would need to be rewritten here.
- Some minor personal preferences :)
    - Inside the commands/queries, I've boxed off model classes in a separate directory.
    - I have created a separate class for the command/query handlers. It's still in the same directory, so I think it still fulfills the concept of having everything related together. I just don't like two classes in one class file :)
- Some very minor code tidying, such as failing fast out of a method when we know a failure condition has been met.

### Infrastructure

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
    - `functional`
        - These are BDD style tests that prove the basic features of the application.
        - I would expect to use little (or no) mocking in these tests.
        - I would expect to use 'real' instances where possible.
        - Potentially have stubs or spies at the boundaries. Specifically where infrastructure pieces are called.
        - In fact, we can probably say that these functional tests will NOT test the infrastructure layer at all (open to debate!)
    - `integration`
        - These specifically DO test the infrastructure layer.
        - They ensure that we interact with various external entities as expected. This is admittedly a little vague and probably requires some further thought...
    - `unit`
        - Pretty much standard unit tests.
        - Use mocking library (moq) but avoid using it for verification.

## How to use this repsitory

The master branch contains a sample application built in the style of Clean Architecture. Use this as a reference to build out your own projects.

If you want a fresh starting point, then fork this repository and start from one of the "StartHere" branches. The latest "StartHere" branch can be found under "StartHere/latest", but you should be able to find all previous versions under the same branch directory (for example, v1.0.0 is found under "StartHere/v1.1.0").