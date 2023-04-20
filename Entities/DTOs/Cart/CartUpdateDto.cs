namespace Entities.DTOs.Cart;

public class CartUpdateDto : IDto
{
    public int Id { get; set; }

    //Relations
    public string UserId { get; set; }
}
