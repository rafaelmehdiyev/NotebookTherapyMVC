namespace Entities.DTOs.ProductSize;

public class ProductSizeGetDto : IDto
{
    public int Id { get; set; }
    public ProductGetDto Product { get; set; }
    public SizeGetDto Size { get; set; }
}
