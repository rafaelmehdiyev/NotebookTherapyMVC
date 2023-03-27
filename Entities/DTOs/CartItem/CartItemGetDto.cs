namespace Entities.DTOs.CartItem;

public class CartItemGetDto : IDto
{
    public int Id { get; set; }
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }


    //Relations
    public ProductGetDto Product { get; set; }
    public CartGetDto Cart { get; set; }
}