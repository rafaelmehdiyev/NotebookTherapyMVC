namespace Entities.DTOs.Cart;

public class CartUpdateDto : IDto
{
    public int Id { get; set; }
    public decimal TotalPrice { get; set; }
    public SaleStatus SaleStatus { get; set; }
    //Relations
    public string UserId { get; set; }
}
