using Project_69_Library.Context;
using Project_69_Library.Models;

namespace Project_69_Library.Repositories
{
    public class AnimalRepository
    {
        public static void Add(string name)
        {
            DB db = new DB();
            db.Animals.Add(new() { Name = name });
            db.SaveChanges();
        }
        public static IEnumerable<Animal> GetAll() => new DB().Animals;
        public static int? GetId(string name) => new DB().Animals.FirstOrDefault(u => u.Name == name)?.Id;
    }
}
