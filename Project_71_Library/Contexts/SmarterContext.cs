using Microsoft.EntityFrameworkCore;
using Project_71_Library.Models;

namespace Project_71_Library.Contexts
{
    public class SmarterContext: DbContext
    {
        public DbSet<User> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=SQL8001.site4now.net;Initial Catalog=db_a8dfee_valik;User Id=db_a8dfee_valik_admin;Password=Vampirlola2020");
        }
    }
}
