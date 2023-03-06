namespace Entities.Concrete;

public class Product : ITable
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
    public bool isDeleted { get; set; }
    public DateTime CreatedDate { get; set; }
    //Relations
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    public int? ProductCollectionId { get; set; }
    public ProductCollection ProductCollection { get; set; }
    public List<ProductImage> ProductImages { get; set; }
    public List<Review> Reviews { get; set; }
}
