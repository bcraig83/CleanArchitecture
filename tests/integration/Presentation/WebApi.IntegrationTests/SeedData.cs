using Domain.Entities;
using Domain.Enums;
using Infrastructure.Persistence;
using System;
using System.Collections.Generic;

namespace WebApi.IntegrationTests
{
    public static class SeedData
    {
        public static void PopulateTestData(ApplicationDbContext dbContext)
        {
            // TODO: seed the db...

            var list = new TodoList
            {
                Colour = "Blue",
                Id = 1,
                Title = "Monday List"
            };

            var todoItem = new TodoItem
            {
                List = list,
                ListId = 1,
                Priority = PriorityLevel.Medium,
                Reminder = DateTime.Parse("24/03/2021"),
                Title = "Buy bread"
            };

            list.Items = new List<TodoItem>
            {
                todoItem
            };

            dbContext.TodoItems.Add(todoItem);
            dbContext.TodoLists.Add(list);

            dbContext.SaveChanges();
        }
    }
}