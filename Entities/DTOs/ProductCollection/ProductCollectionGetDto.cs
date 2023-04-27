namespace Entities.DTOs.ProductCollection;

public class ProductCollectionGetDto : IDto
{
    public int Id { get; set; }
    public string CollectionIcon { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool isDeleted { get; set; }
	//Relations
	public List<ProductGetDto> Products { get; set; }
}