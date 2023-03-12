namespace App.Models
{
    public class CartItem
    {
        public int quantity { get; set; }
        public DateTime addedOn { get; set; }
        public Game? game { get; set; }
    }

    public class Cart
    {
        public int id { get; set; }
        public DateTime createdOn { get; set; }
        public User? user { get; set; }
        public bool isActive { get; set; }
        public List<CartItem>? items { get; set; }
    }
}
