namespace Entities.DTOs.Product;

public class ProductUpdateDto : IDto
{
    public ProductUpdateDto()
    {
        SizeIds = new List<int>();
        BundlesIds = new List<int>();
    }
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int DiscountPercent { get; set; }
    public decimal TotalRating { get; set; }
    //Relations
    public int CategoryId { get; set; }
    public int? ProductCollectionId { get; set; }
    public int? ColorId { get; set; }
    public List<IFormFile> formFiles { get; set; }
    public List<int>? SizeIds { get; set; }
    public List<int>? BundlesIds { get; set; }
}