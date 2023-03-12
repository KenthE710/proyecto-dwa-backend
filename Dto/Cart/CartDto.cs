namespace App.Dto.Cart
{
    public class CartItemGameDto
    {
        public int id { get; set; }
        public string? title { get; set; }
        public string? cover { get; set; }
        public decimal? price { get; set; }
        public bool isActive { get; set; }
        public int stock { get; set; }
    }

    public class CartItemDto
    {
        public int quantity { get; set; }
        public DateTime addedOn { get; set; }
        public CartItemGameDto? game { get; set; }
    }

    public class CartDto
    {
        public int id { get; set; }
        public DateTime createdOn { get; set; }
        public List<CartItemDto>? items { get; set; }
    }
}
