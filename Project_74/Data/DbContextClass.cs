using Microsoft.EntityFrameworkCore;
using Project_74.Models;

namespace Project_74.Data
{
    public class DbContextClass : DbContext
    {
        protected readonly IConfiguration Configuration;
        public DbContextClass(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
        }
        public DbSet<Knife> Knifes { get; set; }
        public DbSet<Admin> Admins { get; set; }
    }
}
