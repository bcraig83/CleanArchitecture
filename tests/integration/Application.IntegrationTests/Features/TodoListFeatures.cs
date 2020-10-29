using Application.TodoLists.Commands.CreateTodoList;
using Application.TodoLists.Commands.DeleteTodoList;
using Domain.Entities;
using FluentValidation;
using Shouldly;
using System;
using Xbehave;
using Xunit;

namespace Application.IntegrationTests.Features
{
    [Collection("Application test collection")]
    public class TodoListFeatures
    {
        private ApplicationTestFixture _fixture;

        public TodoListFeatures(ApplicationTestFixture fixture)
        {
            _fixture = fixture;
        }

        [Scenario]
        public void ShouldThrowException_WhenMinimumFiledsAreNotFilledIn()
        {
            Exception exception = null;
            CreateTodoListCommand command = null;

            "Given an empty command"
                .x(() =>
                {
                    command = new CreateTodoListCommand();
                });

            "When that command is sent"
                .x(async () =>
                {
                    exception = await Record.ExceptionAsync(async () =>
                    {
                        await _fixture.SendAsync(command);
                    });
                });

            "Then the appropriate expection is thrown"
                .x(() =>
                {
                    exception.ShouldBeOfType<ValidationException>();
                    exception.Message.ShouldContain("Title: Title is required.");
                });
        }

        [Scenario]
        public void ShouldThrowException_WhenCreatingListWithNonUniqueTitle()
        {
            int createdListId = 0;
            Exception exception = null;

            "Given a Todo list already exists called 'Shopping'"
                .x(async () =>
                {
                    createdListId = await _fixture.SendAsync(new CreateTodoListCommand
                    {
                        Title = "Shopping"
                    });

                    var createdEntity = await _fixture.FindAsync<TodoList>(createdListId);
                    createdEntity.ShouldNotBeNull();
                });

            "When a new Todo list is injected with the same title"
                .x(async () =>
                {
                    exception = await Record.ExceptionAsync(async () =>
                    {
                        var result = await _fixture.SendAsync(new CreateTodoListCommand
                        {
                            Title = "Shopping"
                        });
                    });
                });

            "Then the appropriate exception should be thrown"
                .x(() =>
                {
                    exception.ShouldBeOfType<ValidationException>();
                    exception.Message.ShouldContain("Title: The specified title already exists.");
                });

            "And we should tidyup after ourselves"
                .x(async () =>
                {
                    await _fixture.SendAsync(new DeleteTodoListCommand
                    {
                        Id = createdListId
                    });
                });
        }

        [Scenario]
        public void ShouldCreateTodoList_WhenValidListIsReceived()
        {
            string userId = null;
            CreateTodoListCommand command = null;
            int createdTodoListId = 0;
            TodoList createdList = null;

            "Given that we are running as the default user"
                .x(async () =>
                {
                    userId = await _fixture.RunAsDefaultUserAsync();
                });

            "And given a valid command to create a new TodoList"
                .x(() =>
                {
                    command = new CreateTodoListCommand()
                    {
                        Title = "New List"
                    };
                });

            "When that command is injected"
                .x(async () =>
                {
                    createdTodoListId = await _fixture.SendAsync(command);
                });

            "Then we should be able to retrieve the newly created list"
                .x(async () =>
                {
                    createdList = await _fixture.FindAsync<TodoList>(createdTodoListId);
                });

            "And the list should be correct"
                .x(() =>
                {
                    createdList.ShouldNotBeNull();
                    createdList.Title.ShouldBe(command.Title);
                    createdList.CreatedBy.ShouldBe(userId);
                });
        }
    }
}