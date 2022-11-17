namespace Project_73.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public string? ProductDescription { get; set; }
        public int ProductCost { get; set; }
        public int ProductStock { get; set; }
        public int ProductDiscount { get; set; }
        public string? ProductImageUrl { get; set; }
    }
}
