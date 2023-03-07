namespace Entities.DTOs.ProductCollection;

public class ProductCollectionGetDto : IDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    //Relations
    public List<ProductGetDto> Products { get; set; }
}