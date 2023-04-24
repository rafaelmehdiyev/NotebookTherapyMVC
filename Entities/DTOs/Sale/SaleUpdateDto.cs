namespace Entities.DTOs.Sale;

public class SaleUpdateDto : IDto
{
    public int Id { get; set; }
    public decimal TotalPrice { get; set; }
    public SaleStatus SaleStatus { get; set; }
    public string UserId { get; set; }
}
