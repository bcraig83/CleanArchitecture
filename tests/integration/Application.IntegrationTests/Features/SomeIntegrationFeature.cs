using Infrastructure.Persistence;
using Xbehave;

namespace IntegrationTests.Features
{
    public class SomeIntegrationFeature : BaseInMemoryDatabaseTests
    {
        [Scenario]
        public void ShouldSendEmailWhenTodoItemIsMarkedAsDone()
        {
            "Given..."
                .x(() =>
                {
                });

            "When..."
                .x(() =>
                {
                });

            "Then..."
                .x(() =>
                {
                });
        }

        protected override void SeedFeatureSpecificData(ApplicationDbContext context)
        {
            // Add specific code here, e.g.
            // var item = new Entity();
            //context.Add(item);
            //context.SaveChanges();
        }
    }
}