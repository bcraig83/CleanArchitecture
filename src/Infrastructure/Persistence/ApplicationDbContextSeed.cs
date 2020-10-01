using Domain.Entities;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedDefaultUserAsync(UserManager<ApplicationUser> userManager)
        {
            var defaultUser = new ApplicationUser { UserName = "administrator@localhost", Email = "administrator@localhost" };

            if (userManager.Users.All(u => u.UserName != defaultUser.UserName))
            {
                await userManager.CreateAsync(defaultUser, "Administrator1!");
            }
        }

        public static async Task SeedSampleDataAsync(ApplicationDbContext2 context)
        {
            // Seed, if necessary
            if (!context.TodoLists.Any())
            {
                context.TodoLists.Add(new TodoList
                {
                    Title = "Shopping",
                    Items = GetTodoItems()
                });

                await context.SaveChangesAsync();
            }
        }

        private static IList<TodoItem> GetTodoItems()
        {
            // TODO: have a think about this. I think it's ok, because this is setting up seed data.
            // In a real system, a TodoItem could never be created and set to done in one go.
            var apples = new TodoItem { Title = "Apples" };
            apples.MarkComplete();

            var milk = new TodoItem { Title = "Apples" };
            milk.MarkComplete();

            var bread = new TodoItem { Title = "Bread" };
            bread.MarkComplete();

            var result = new List<TodoItem>{
                        apples,
                        milk,
                        bread,
                        new TodoItem { Title = "Toilet paper" },
                        new TodoItem { Title = "Pasta" },
                        new TodoItem { Title = "Tissues" },
                        new TodoItem { Title = "Tuna" },
                        new TodoItem { Title = "Water" }
                    };

            return result;
        }
    }
}