using System;
using Xbehave;

namespace IntegrationTests.Features
{
    public class EmailNotificationFeature : BaseInMemoryDatabaseTests
    {
        [Scenario]
        public void ShouldDoSomething()
        {
            "Given something..."
                .x(() =>
                {
                    Console.WriteLine("Hello world...");
                });
        }
    }
}