namespace Entities.DTOs.Product;

public class ProductUpdateDto : IDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int DiscountPercent { get; set; }
    public bool isSale { get; set; }
    public Color Color { get; set; }
    public Size Size { get; set; }
    public Bundle Bundle { get; set; }
    //Relations
    public int CategoryId { get; set; }
    public int? ProductCollectionId { get; set; }
    public List<IFormFile> formFiles { get; set; }
}