using Microsoft.AspNetCore.Identity;
using shop.data;
using shop.Models;
using System.Net;

namespace shop.Data
{
    public class Seed
    {
        public static void SeedGames(IApplicationBuilder applicationBuilder)
        {
            using (var scope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                if (!dbContext.Games.Any())
                {
                    var games = new List<Game>
                    { 
                        new Game { Name = "The Witcher 3: Wild Hunt", Description = "Open-world RPG", Genre = "RPG", IsOnSale = true, Price = 49.99m, PriceAfterSale = 44.99m, ImagePath = "/images/w3.jpg", Alt = "witcher3"},
                        new Game { Name = "Grand Theft Auto V", Description = "Open-world action-adventure", Genre = "Action", IsOnSale = false, Price = 29.99m, ImagePath = "/images/gtav.jpg", Alt = "gtaV"},
                        new Game { Name = "Red Dead Redemption 2", Description = "Wild West adventure", Genre = "Action-Adventure", IsOnSale = false, Price = 59.99m, ImagePath = "/images/rd2.jpg", Alt = "rd2" },
                        new Game { Name = "The Elder Scrolls V: Skyrim", Description = "Epic fantasy RPG", Genre = "RPG", IsOnSale = false, Price = 39.99m, ImagePath = "/images/skyrim.jpg", Alt = "skyrim" },
                        new Game { Name = "Dark Souls III", Description = "Challenging action RPG", Genre = "Action-RPG", IsOnSale = true, Price = 39.99m, PriceAfterSale = 29.99m, ImagePath = "/images/ds3.jpg", Alt = "ds3" },
                        new Game { Name = "Fallout 4", Description = "Post-apocalyptic RPG", Genre = "RPG", IsOnSale = false, Price = 19.99m, ImagePath = "/images/fallout4.jpg", Alt = "fallout4" },
                        new Game { Name = "Assassin's Creed Odyssey", Description = "Ancient Greek action-adventure", Genre = "Action-Adventure", IsOnSale = true, Price = 49.99m, PriceAfterSale = 39.99m, ImagePath = "/images/ac_odyssey.jpg", Alt = "ac_odyssey" },
                        new Game { Name = "Minecraft", Description = "Sandbox survival", Genre = "Sandbox", IsOnSale = false, Price = 24.99m, ImagePath = "/images/minecraft.jpg", Alt = "minecraft" },
                    };


                    dbContext.Games.AddRange(games);
                    dbContext.SaveChanges();
                }
            }
        }

        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                string adminUserEmail = "teddysmithdeveloper@gmail.com";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new AppUser()
                    {
                        UserName = "teddysmithdev",
                        Email = adminUserEmail,
                        EmailConfirmed = true,
                        Address = new Address()
                        {
                            Street = "123 Main St",
                            City = "Charlotte",
                            State = "NC"
                        }
                    };
                    await userManager.CreateAsync(newAdminUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }

                string appUserEmail = "user@etickets.com";

                var appUser = await userManager.FindByEmailAsync(appUserEmail);
                if (appUser == null)
                {
                    var newAppUser = new AppUser()
                    {
                        UserName = "app-user",
                        Email = appUserEmail,
                        EmailConfirmed = true,
                        Address = new Address()
                        {
                            Street = "123 Main St",
                            City = "Charlotte",
                            State = "NC"
                        }
                    };
                    await userManager.CreateAsync(newAppUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                }
            }
        }
    }
}
