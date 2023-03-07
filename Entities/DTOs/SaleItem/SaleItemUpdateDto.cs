namespace Entities.DTOs.SaleItem;

public class SaleItemUpdateDto : IDto
{
    public int Id { get; set; }
    public int Quantity { get; set; }

    //Relations
    public int ProductId { get; set; }
    public int SaleId { get; set; }
}
