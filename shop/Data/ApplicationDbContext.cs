using Microsoft.EntityFrameworkCore;
using shop.Models;

namespace shop.data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Game> Games { get; set; }
    }
}
