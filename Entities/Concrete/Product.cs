namespace Entities.Concrete;
public class Product : ITable
{
    public Product()
    {
        ProductImages = new List<ProductImage>();
        ProductSizes = new List<ProductSize>();
        ProductBundles = new List<ProductBundle>();
    }
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal LastPrice { get; set; }
    public int DiscountPercent { get; set; }
    public decimal Price { get; set; }
    public decimal TotalRating { get; set; }
    public bool isSale { get; set; }
    public bool isDeleted { get; set; }
    public DateTime CreatedDate { get; set; }
    //Relations
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    public int? ProductCollectionId { get; set; }
    public ProductCollection? ProductCollection { get; set; }
    public int? ColorId { get; set; }
    public Color? Color { get; set; }
    public List<ProductImage> ProductImages { get; set; }
    public List<CartItem> CartItems { get; set; }
    public List<SaleItem> SaleItems { get; set; }
    public List<Review> Reviews { get; set; }
    public List<Favourite> Favourites { get; set; }
    public List<ProductSize> ProductSizes { get; set; }
    public List<ProductBundle> ProductBundles { get; set; }


}
