TodoFeature includes a functional test, `ShouldBeAbleToCreateTodoItem()`.
Admittedly, this is quite simple. But in a real application a handler might be passing it
to various different services in the application and domain layer. The idea of these tests
is to stub the end points (in other words, the database) but use real implementations 
everywhere else. In this way, it's hoped that 60%-70% of the functionaliy could be tested
without having to go near a database. And since it's done with the xunit runner, they result
in an incredibly short feedback loop :-)

I'll add more complex examples as the project evolves.
