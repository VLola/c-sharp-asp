namespace Project_68.Models
{
    public class Note
    {
        public int Id { get; set; }
        public string Owner { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
