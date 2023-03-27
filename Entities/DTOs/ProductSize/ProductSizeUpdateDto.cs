namespace Entities.DTOs.ProductSize;

public class ProductSizeUpdateDto : IDto
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int SizeId { get; set; }
}
