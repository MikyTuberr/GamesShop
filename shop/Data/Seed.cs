using shop.data;
using shop.Models;

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
                        new Game { Id = "1", Name = "The Witcher 3: Wild Hunt", Description = "Open-world RPG", IsOnSale = true, Price = 49.99m, PriceAfterSale = 44.99m, ImagePath = "/images/w3.jpg", Alt = "witcher3"},
                        new Game { Id = "2", Name = "Grand Theft Auto V", Description = "Open-world action-adventure", IsOnSale = false, Price = 29.99m, ImagePath = "/images/gtav.jpg", Alt = "gtaV"},
                        new Game { Id = "3", Name = "Red Dead Redemption 2", Description = "Wild West adventure", IsOnSale = false, Price = 59.99m, ImagePath = "/images/rd2.jpg", Alt = "rd2" },
                        // Dodaj tutaj pozostałe gry
                    };

                    dbContext.Games.AddRange(games);
                    dbContext.SaveChanges();
                }
            }
        }
    }
}
