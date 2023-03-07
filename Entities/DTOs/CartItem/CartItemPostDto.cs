namespace Entities.DTOs.CartItem
{
    public class CartItemPostDto : IDto
    {
        public string Name { get; set; }
        public int Quantity { get; set; }

        //Relations
        public int ProductId { get; set; }
        public int CartId { get; set; }
    }
}