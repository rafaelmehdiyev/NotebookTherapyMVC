namespace DataAccessLayer.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private IBlogRepository _blogRepository;
    private ICartRepository _cartRepository;
    private ICartItemRepository _cartItemRepository;
    private ICategoryRepository _categoryRepository;
    private IFAQRepository _faqRepository;
    private IFAQCategoryRepository _faqCategoryRepository;
    private IFavouriteRepository _favouriteRepository;
    private IProductCollectionRepository _productCollectionRepository;
    private IProductImageRepository _productImageRepository;
    private IProductRepository _productRepository;
    private ISizeRepository _sizeRepository;
    private IProductSizeRepository _productSizeRepository;
    private IBundleRepository _bundleRepository;
    private IProductBundleRepository _productBundleRepository;
    private IColorRepository _colorRepository;
    private IReviewRepository _reviewRepository;
    private ISaleRepository _saleRepository;
    private ISaleItemRepository _saleItemRepository;
    private IShippingRepository _shippingRepository;
    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }
    public IBlogRepository BlogRepository => _blogRepository = _blogRepository ?? new EfBlogRepository(_context);
    public ICartRepository CartRepository => _cartRepository = _cartRepository ?? new EfCartRepository(_context);
    public ICartItemRepository CartItemRepository => _cartItemRepository = _cartItemRepository ?? new EfCartItemRepository(_context);
    public ICategoryRepository CategoryRepository => _categoryRepository = _categoryRepository ?? new EfCategoryRepository(_context);
    public IFAQRepository FAQRepository => _faqRepository = _faqRepository ?? new EfFAQRepository(_context);
    public IFAQCategoryRepository FAQCategoryRepository => _faqCategoryRepository = _faqCategoryRepository ?? new EfFAQCategoryRepository(_context);
    public IFavouriteRepository FavouriteRepository => _favouriteRepository = _favouriteRepository ?? new EfFavouriteRepository(_context);
    public IProductCollectionRepository ProductCollectionRepository => _productCollectionRepository = _productCollectionRepository ?? new EfProductCollectionRepository(_context);
    public IProductImageRepository ProductImageRepository => _productImageRepository = _productImageRepository ?? new EfProductImageRepository(_context);
    public IProductRepository ProductRepository => _productRepository = _productRepository ?? new EfProductRepository(_context);
    public IProductSizeRepository ProductSizeRepository => _productSizeRepository = _productSizeRepository ?? new EfProductSizeRepository(_context);
    public IProductBundleRepository ProductBundleRepository => _productBundleRepository = _productBundleRepository ?? new EfProductBundleRepository(_context);
    public ISizeRepository SizeRepository => _sizeRepository = _sizeRepository ?? new EfSizeRepository(_context);
    public IBundleRepository BundleRepository => _bundleRepository = _bundleRepository ?? new EfBundleRepository(_context);
    public IColorRepository ColorRepository => _colorRepository = _colorRepository ?? new EfColorRepository(_context);
    public IReviewRepository ReviewRepository => _reviewRepository = _reviewRepository ?? new EfReviewRepository(_context);
    public ISaleRepository SaleRepository => _saleRepository = _saleRepository ?? new EfSaleRepository(_context);
    public ISaleItemRepository SaleItemRepository => _saleItemRepository = _saleItemRepository ?? new EfSaleItemRepository(_context);
    public IShippingRepository ShippingRepository => _shippingRepository = _shippingRepository ?? new EfShippingRepository(_context);
    public void Dispose()
    {
        _context.Dispose();
    }
    public async Task<int> SaveAsync()
    {
        return await _context.SaveChangesAsync();
    }
}
