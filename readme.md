# Clean Architecture Solution Template

This repo is based on [this](https://github.com/jasontaylordev/CleanArchitecture).

## What's tweaked

### General

- All projects have been upgraded to use dot net core 3.1.

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