namespace Entities.DTOs.SaleItem;

public class SaleItemPostDto : IDto
{
    public int Quantity { get; set; }

    //Relations
    public int ProductId { get; set; }
    public int SaleId { get; set; }
}
