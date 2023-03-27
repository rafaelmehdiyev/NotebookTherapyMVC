namespace Entities.DTOs.Bundle;

public class BundleGetDto : IDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    //Relations
    public List<ProductBundleGetDto> ProductBundles { get; set; }
}
