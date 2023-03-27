namespace DataAccessLayer.Repositories;

public interface IUnitOfWork : IDisposable
{
    public IBlogRepository BlogRepository { get; }
    public ICartRepository CartRepository { get; }
    public ICartItemRepository CartItemRepository { get; }
    public ICategoryRepository CategoryRepository { get; }
    public IFAQRepository FAQRepository { get; }
    public IFAQCategoryRepository FAQCategoryRepository { get; }
    public IFavouriteRepository FavouriteRepository { get; }
    public IProductCollectionRepository ProductCollectionRepository { get; }
    public IProductImageRepository ProductImageRepository { get; }
    public IProductRepository ProductRepository { get; }
    public ISizeRepository SizeRepository { get; }
    public IBundleRepository BundleRepository { get; }
    public IProductSizeRepository ProductSizeRepository { get; }
    public IProductBundleRepository ProductBundleRepository { get; }
    public IColorRepository ColorRepository { get; }
    public IReviewRepository ReviewRepository { get; }
    public ISaleRepository SaleRepository { get; }
    public ISaleItemRepository SaleItemRepository { get; }
    public IShippingRepository ShippingRepository { get; }

    Task<int> SaveAsync();
}
