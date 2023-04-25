namespace Entities.DTOs.SaleItem;

public class SaleItemGetDto : IDto
{
    public int Id { get; set; }
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }
	public bool isDeleted { get; set; }
    public DateTime CreatedDate { get; set; }

    //Relations
    public int ProductId { get; set; }
    public ProductGetDto Product { get; set; }
    public int SaleId { get; set; }
    public SaleGetDto Sale { get; set; }
}