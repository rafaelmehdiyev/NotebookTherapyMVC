namespace Entities.DTOs.Product;

public class ProductGetDto : IDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal LastPrice { get; set; }
    public int DiscountPercent { get; set; }
    public decimal Price { get; set; }
	public bool isSale { get; set; }
	public bool isDeleted { get; set; }
	//Relations
	public CategoryGetDto Category { get; set; }
    public ProductCollectionGetDto? ProductCollection { get; set; }
    public ColorGetDto? Color { get; set; }
    public List<ProductImage> ProductImages { get; set; }
    public List<CartItemGetDto> CartItems { get; set; }
    public List<ReviewGetDto> Reviews { get; set; }
    public List<ProductSizeGetDto> ProductSizes { get; set; }
    public List<ProductBundleGetDto> ProductBundles { get; set; }
    public List<FavouriteGetDto> Favourites { get; set; }
}
