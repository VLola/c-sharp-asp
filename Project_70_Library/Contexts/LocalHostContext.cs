using Microsoft.EntityFrameworkCore;
using Project_70_Library.Models;
namespace Project_70_Library.Contexts
{
    public class LocalHostContext : DbContext
    {
        public DbSet<Note> Notes { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-TBFG5D3\\SQLEXPRESS;Initial Catalog=Asp;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }
    }
}
