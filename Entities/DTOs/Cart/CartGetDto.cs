namespace Entities.DTOs.Cart;

public class CartGetDto : IDto
{
    public int Id { get; set; }

    //Relations
    public List<CartItemGetDto> CartItems { get; set; }
    public AppUser User { get; set; }
}
