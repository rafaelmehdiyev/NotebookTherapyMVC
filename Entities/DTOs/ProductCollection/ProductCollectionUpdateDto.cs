namespace Entities.DTOs.ProductCollection;

public class ProductCollectionUpdateDto : IDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string CollectionHeaderImage { get; set; }
    public string CollectionItemBackgroundImage { get; set; }
    public string CollectionFooterImage { get; set; }
    public string CollectionColor { get; set; }
    public string CollectionItemColor { get; set; }
    public string CollectionButtonColor { get; set; }
}