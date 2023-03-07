namespace Entities.DTOs.CartItem
{
    public class CartItemPostDto : IDto
    {
        public int Quantity { get; set; }

        //Relations
        public int ProductId { get; set; }
        public int CartId { get; set; }
    }
}