using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.EntityFramework
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

        public static async Task SeedSampleDataAsync(ApplicationDbContext context)
        {
            // Seed, if necessary, for example:
            //if (!context.TodoLists.Any())
            //{
            //    context.TodoLists.Add(new TodoList
            //    {
            //        Title = "Shopping",
            //        Items = GetTodoItems()
            //    });

            //    await context.SaveChangesAsync();
            //}
        }
    }
}