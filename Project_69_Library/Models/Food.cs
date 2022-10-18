using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Project_69_Library.Models
{
    public class Food
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UrlImage { get; set; }
        public decimal Price { get; set; }
        public int? AnimalId { get; set; }
        public Animal Animal { get; set; }
    }
}
