namespace Entities.DTOs.ProductBundle;

public class ProductBundleGetDto : IDto
{
    public int Id { get; set; }
    public ProductGetDto Product { get; set; }
    public BundleGetDto Bundle { get; set; }
}