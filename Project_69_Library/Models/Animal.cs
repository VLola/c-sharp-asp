namespace Project_69_Library.Models
{
    public class Animal
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Food> Foods { get; set; } = new();
    }
}
