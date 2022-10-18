using Project_69_Library.Context;
using Project_69_Library.Models;

namespace Project_69_Library.Repositories
{
    public static class FoodRepository
    {
        public static void Add(string animalName, string foodName, decimal price)
        {
            DB db = new DB();
            Food food = new Food();
            food.Name = foodName;
            food.Price = price;

            int? id = AnimalRepository.GetId(animalName);

            if (id == null) food.Animal = new() { Name = animalName };
            else food.AnimalId = id;

            db.Foods.Add(food);
            db.SaveChanges();
        }
        public static IEnumerable<Food> GetAll() => new DB().Foods;
    }
}
