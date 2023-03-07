namespace Entities.DTOs.SaleItem;

public class SaleItemGetDto : IDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Quantity { get; set; }

    //Relations
    public int ProductId { get; set; }
    public ProductGetDto Product { get; set; }
    public int SaleId { get; set; }
    public SaleGetDto Sale { get; set; }
}