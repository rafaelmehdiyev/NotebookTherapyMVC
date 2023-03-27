namespace Entities.DTOs.Cart;

public class CartPostDto : IDto
{
    public decimal TotalPrice { get; set; }
    //Relations
    public string UserId { get; set; }
}
