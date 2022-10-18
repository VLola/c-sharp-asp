using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Project_69_Library.Models;

namespace Project_69_Library.Context
{
    public class DB : DbContext
    {
        public DbSet<Food> Foods { get; set; }
        public DbSet<Animal> Animals { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            new ConfigurationBuilder().AddUserSecrets<DB>()
                .Build()
                .Providers
                .First()
                .TryGet("connStr", out var connStr);
            optionsBuilder.UseSqlServer(connStr);
        }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Food>()
        //        .HasOne(p => p.Animal)
        //        .WithMany(t => t.Foods)
        //        .HasForeignKey(p => p.AnimalId);
        //}
    }
}
