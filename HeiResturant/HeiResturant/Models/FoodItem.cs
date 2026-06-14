namespace HeiResturant.Models
{
    public class FoodItem
    {
        public int FoodId { get; set; }
        public int RestaurantId { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public bool IsAvailable { get; set; }
    }
}
