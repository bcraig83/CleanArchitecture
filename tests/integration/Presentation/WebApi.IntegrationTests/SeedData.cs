﻿using Infrastructure.Persistence;

namespace WebApi.IntegrationTests
{
    public static class SeedData
    {
        public static void PopulateTestData(ApplicationDbContext dbContext)
        {
            // TODO: popualate data as required, e.g.

            //var list = new TodoList
            //{
            //    Colour = "Blue",
            //    Id = 1,
            //    Title = "Monday List"
            //};

            //var todoItem = new TodoItem
            //{
            //    List = list,
            //    ListId = 1,
            //    Priority = PriorityLevel.Medium,
            //    Reminder = DateTime.Now,
            //    Title = "Buy bread"
            //};

            //list.Items = new List<TodoItem>
            //{
            //    todoItem
            //};

            //dbContext.TodoItems.Add(todoItem);
            //dbContext.TodoLists.Add(list);

            dbContext.SaveChanges();
        }
    }
}