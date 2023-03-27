namespace Entities.DTOs.Sale;

public class SalePostDto : IDto
{
    public decimal TotalPrice { get; set; }
    public string UserId { get; set; }
}
