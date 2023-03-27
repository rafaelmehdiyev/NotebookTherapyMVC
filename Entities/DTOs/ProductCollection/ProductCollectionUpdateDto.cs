namespace Entities.DTOs.ProductCollection;

public class ProductCollectionUpdateDto : IDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public IFormFile CollectionHeaderImage { get; set; }
    public IFormFile CollectionItemBackgroundImage { get; set; }
    public IFormFile CollectionFooterImage { get; set; }
    public string CollectionColor { get; set; }
    public string CollectionItemColor { get; set; }
    public string CollectionButtonColor { get; set; }
}