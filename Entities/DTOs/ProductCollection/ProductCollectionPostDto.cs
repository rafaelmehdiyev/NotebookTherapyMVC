namespace Entities.DTOs.ProductCollection;

public class ProductCollectionPostDto : IDto
{
    public string CollectionIcon { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}
