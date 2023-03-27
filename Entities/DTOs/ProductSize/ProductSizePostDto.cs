namespace Entities.DTOs.ProductSize;

public class ProductSizePostDto : IDto
{
    public int ProductId { get; set; }
    public int SizeId { get; set; }
}
