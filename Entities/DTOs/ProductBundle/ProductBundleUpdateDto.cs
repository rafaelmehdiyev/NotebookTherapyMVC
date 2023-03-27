namespace Entities.DTOs.ProductBundle;

public class ProductBundleUpdateDto : IDto
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int BundleId { get; set; }
}