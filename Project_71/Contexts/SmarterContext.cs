using Microsoft.EntityFrameworkCore;
using Project_71_Library.Models;

namespace Project_71.Contexts
{
    public class SmarterContext: DbContext
    {
        public DbSet<User> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build()
                .GetConnectionString("SmarterHost"));
        }
    }
}
