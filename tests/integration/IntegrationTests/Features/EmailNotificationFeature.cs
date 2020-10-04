using Application.Common.Interfaces;
using Application.TodoItems.Commands.UpdateTodoItem;
using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Persistence;
using IntegrationTests.Fakes;
using Shouldly;
using System.Linq;
using Xbehave;

namespace IntegrationTests.Features
{
    public class EmailNotificationFeature : BaseInMemoryDatabaseTests
    {
        [Scenario]
        public void ShouldSendEmailWhenTodoItemIsMarkedAsDone()
        {
            UpdateTodoItemCommand command = null;
            EmailSenderSpy emailSenderSpy = (EmailSenderSpy)ServiceProvider.GetService(typeof(IEmailSender)); ;

            "Given no emails have intially been sent"
                .x(() =>
                {
                    var recordedEmails = emailSenderSpy.RecordedEmails;
                    recordedEmails.Count.ShouldBe(0);
                });

            "And given the Todo item exists in the database"
                .x(async () =>
                {
                    var todoItemRepo = (ITodoItemRepository)ServiceProvider.GetService(typeof(ITodoItemRepository));
                    var todoItem = await todoItemRepo.FindByIdAsync(23);
                    todoItem.ShouldNotBeNull();
                    todoItem.Title.ShouldBe("Pick up paint");
                    todoItem.IsDone.ShouldBeFalse();
                });

            "And given a command for a pre-existing TodoItem"
                .x(() =>
                {
                    command = new UpdateTodoItemCommand
                    {
                        Id = 23,
                        Done = true
                    };
                });

            "When that is sent to the handler"
                .x(async () =>
                {
                    var result = await Mediator.Send(command);
                    result.ShouldNotBeNull();
                });

            "Then an email update should be sent"
                .x(() =>
                {
                    var recordedEmails = emailSenderSpy.RecordedEmails;
                    recordedEmails.Count.ShouldBe(1);

                    var sentEmail = recordedEmails.FirstOrDefault();
                    sentEmail.ShouldNotBeNull();

                    sentEmail.To.ShouldBe("test@test.com");
                    sentEmail.From.ShouldBe("test@test.com");
                    sentEmail.Subject.ShouldBe("Pick up paint was completed.");
                    sentEmail.Body.ShouldBe("Domain.Entities.TodoItem");
                });
        }

        protected override void SeedFeatureSpecificData(ApplicationDbContext context)
        {
            var existingTodo = new TodoItem
            {
                Title = "Pick up paint",
                Id = 23
            };

            context.Add(existingTodo);
            context.SaveChanges();
        }
    }
}