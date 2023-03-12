namespace App.Dto.Cart
{
    public class AddToCartDto
    {
        public int quantity { get; set; }
        public int userId { get; set; }
        public int gameId { get; set; }
    }
}
