namespace Entities.DTOs.CartItem
{
    public class CartItemUpdateDto : IDto
    {
        public int Id { get; set; }
        public int Quantity { get; set; }

        //Relations
        public int ProductId { get; set; }
        public int CartId { get; set; }
    }
}