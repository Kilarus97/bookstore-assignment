using BookstoreApplication.Models;
using Microsoft.AspNetCore.Identity;

namespace BookstoreApplication.Data
{
    public static class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();

            // Kreiranje prvog administratora
            var editor1 = await userManager.FindByNameAsync("john");
            if (editor1 == null)
            {
                editor1 = new User
                {
                    UserName = "john",
                    Email = "john.doe@example.com",
                    Name = "John",
                    Surname = "Doe",
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(editor1, "John123!");
            }

            if (!await userManager.IsInRoleAsync(editor1, "Editor"))
            {
                await userManager.AddToRoleAsync(editor1, "Editor");
            }

            // Kreiranje drugog administratora
            var editor2 = await userManager.FindByNameAsync("jane");
            if (editor2 == null)
            {
                editor2 = new User
                {
                    UserName = "jane",
                    Email = "jane.doe@example.com",
                    Name = "Jane",
                    Surname = "Doe",
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(editor2, "Jane123!");
            }

            if (!await userManager.IsInRoleAsync(editor2, "Editor"))
            {
                await userManager.AddToRoleAsync(editor2, "Editor");
            }

            // Kreiranje treceg administratora
            var editor3 = await userManager.FindByNameAsync("nick");
            if (editor3 == null)
            {
                editor3 = new User
                {

                    UserName = "nick",
                    Email = "nick.smith@example.com",
                    Name = "Nick",
                    Surname = "Smith",
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(editor3, "Nick123!");
            }
            ;

            if (!await userManager.IsInRoleAsync(editor3, "Editor"))
            {
                await userManager.AddToRoleAsync(editor3, "Editor");
            }
        }
    }

}
