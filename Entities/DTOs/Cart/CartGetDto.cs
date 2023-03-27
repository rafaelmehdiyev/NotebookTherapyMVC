namespace Entities.DTOs.Cart;

public class CartGetDto : IDto
{
    public int Id { get; set; }
    public decimal TotalPrice { get; set; }

    //Relations
    public List<CartItemGetDto> CartItems { get; set; }
    public UserGetDto User { get; set; }
}
