namespace NotebookTherapyMVC.Controllers;

public class ProductController : Controller
{
    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;
    private readonly ISizeService _sizeService;
    private readonly IBundleService _bundleService;
    private readonly IProductCollectionService _productCollectionService;
    private readonly IFavouriteService _favService;
    private readonly IAccountService _accountService;
    private readonly IReviewService _reviewService;
    private readonly ICartItemService _cartItemService;
    private readonly ICartService _cartService;
    private readonly IMapper _mapper;

    public ProductController(IProductService productService, ICategoryService categoryService,
        ISizeService sizeService, IBundleService bundleService,
        IProductCollectionService productCollectionService, IFavouriteService favService,
        IAccountService accountService, IReviewService reviewService, ICartItemService cartItemService, ICartService cartService, IMapper mapper)
    {
        _productService = productService;
        _categoryService = categoryService;
        _sizeService = sizeService;
        _bundleService = bundleService;
        _productCollectionService = productCollectionService;
        _favService = favService;
        _accountService = accountService;
        _reviewService = reviewService;
        _cartItemService = cartItemService;
        _cartService = cartService;
        _mapper = mapper;
    }

    public async Task<IActionResult> Detail(int id)
    {
        IDataResult<ProductGetDto> result = await _productService.GetByIdAsync(id, Includes.ProductIncludes);
        return View(result);
    }

    public async Task<IActionResult> GetRelated()
    {
        IDataResult<List<ProductGetDto>> result = await _productService.GetAllAsync(false, "ProductImages");
        List<ProductGetDto> related = result.Data.OrderBy(x => Guid.NewGuid()).ToList();
        return PartialView("_productsCarouselPartial", related);
    }

    #region Filters
    public async Task<IActionResult> Index(int? categoryId, bool? isTrending, bool? onSale)
    {
        FilterProductVM filter = categoryId is not null ? new FilterProductVM((int)categoryId)
                                : (isTrending is not null && onSale is not null) ? new FilterProductVM((bool)onSale, (bool)isTrending)
                                : isTrending is not null ? new FilterProductVM(isTrending: (bool)isTrending)
                                : onSale is not null ? new FilterProductVM(onSale: (bool)onSale)
                                : new FilterProductVM();
        List<ProductGetDto> filteredProducts = await GetFilteredProducts(filter);
        return View(filteredProducts);
    }

    public async Task<IActionResult> GetProductsPartialAsync(FilterProductVM filter)
    {
        List<ProductGetDto> filteredProducts = await GetFilteredProducts(filter);
        return PartialView("_productListPartial", filteredProducts);
    }

    #region Private Methods
    private async Task<List<ProductGetDto>> GetFilteredProducts(FilterProductVM filter)
    {
        List<ProductGetDto> products = (await _productService.GetAllAsync(false, Includes.ProductIncludes)).Data;
        //Checking Product is on sale or not
        products = products.Where(p => p.isSale == filter.onSale).ToList();
        //Price Range Filter
        products = products.Where(p => p.Price <= filter.priceRange).ToList();
        if (new[] { filter.categoryIds, filter.colorIds, filter.bundleIds, filter.sizeIds }.Any(a => a != null && a.Length >= 0))
        {
            if (filter.categoryIds is not null)
            {
                products = products.Where(p => filter.categoryIds.Contains(p.Category.Id)).ToList();
            }
            if (filter.colorIds is not null)
            {
                products = products.Where(p => filter.colorIds.Contains(p.Color.Id)).ToList();
            }
            if (filter.sizeIds is not null)
            {
                products = products.Where(p => p.ProductSizes.Any(ps => filter.sizeIds.Contains(ps.Size.Id))).ToList();
            }
            if (filter.bundleIds is not null)
            {
                products = products.Where(p => p.ProductBundles.Any(ps => filter.bundleIds.Contains(ps.Bundle.Id))).ToList();
            }
        }
        //Order Products
        products = filter.orderFilter switch
        {
            //Most Relevant
            0 => products,
            //Trending
            //1=>products.OrderByDescending(p=>p.ViewCount).ToList(),
            //The Lowest Price
            2 => products.OrderBy(p => p.Price).ToList(),
            //The Highest Price
            3 => products.OrderByDescending(p => p.Price).ToList(),
            //The Newest
            4 => products.OrderByDescending(p => p.CreatedDate).ToList(),
            //Bestsellers
            //5 => products.OrderBy().ToList(),
            //Most Liked
            6 => products.OrderByDescending(p => p.TotalRating).ToList(),
        };
        return products;
    }

    #endregion

    #endregion

    #region Favourite
    [HttpPost]
    [Authorize(Roles = "SuperAdmin,Admin,User")]
    public async Task<IDataResult<FavouriteGetDto>> AddToFavourite(int id)
    {
        FavouritePostDto dto = new()
        {
            ProductId = id,
            UserId = (await _accountService.GetUserByClaims(User, Includes.UserIncludes)).Data.Id
        };
        var result = await _favService.CreateAsync(dto);
        return result;
    }

    [HttpPost]
    [Authorize(Roles = "SuperAdmin,Admin,User")]
    public async Task<IResult> RemoveFromFavourite(int id)
    {
        var result = await _favService.HardDeleteByIdAsync(id);
        return result;
    }
    #endregion

    #region Shopping Cart

    [HttpPost]
    [Authorize(Roles = "SuperAdmin,Admin,User")]
    public async Task<IDataResult<CartItemGetDto>> AddItemToCart(int id)
    {
        ProductGetDto productDto = (await _productService.GetByIdAsync(id)).Data;
        UserGetDto user = (await _accountService.GetUserByClaims(User, Includes.UserIncludes)).Data;
        IDataResult<CartItemGetDto> result = await _cartItemService.CreateAsync(productDto, user);
        CartUpdateDto cartDto = _mapper.Map<CartUpdateDto>(user.Cart);
        cartDto.TotalPrice += productDto.Price;
        IResult cartResult = await _cartService.UpdateAsync(cartDto);
        return result;
    }

    [HttpPost]
    [Authorize(Roles = "SuperAdmin,Admin,User")]
    public async Task<IDataResult<CartItemGetDto>> RemoveItemFromCart(int id, bool deleteAll = false)
    {
        ProductGetDto productDto = (await _productService.GetByIdAsync(id)).Data;
        UserGetDto user = (await _accountService.GetUserByClaims(User, Includes.UserIncludes)).Data;
        IDataResult<CartItemGetDto> result = await _cartItemService.RemoveItemFromCartAsync(productDto, user, deleteAll);
        CartUpdateDto cartDto = _mapper.Map<CartUpdateDto>(user.Cart);
        if (deleteAll) { cartDto.TotalPrice = 0; }
        else { cartDto.TotalPrice -= productDto.Price; }
        IResult cartResult = await _cartService.UpdateAsync(cartDto);
        return result;
    }

    #endregion

    [HttpPost]
    [Authorize(Roles = "SuperAdmin,Admin,User")]
    public async Task<IActionResult> WriteComment(ReviewPostDto dto)
    {
        dto.UserId = (await _accountService.GetUserByClaims(User)).Data.Id;
        IResult result = await _reviewService.CreateAsync(dto);
        return RedirectToAction("Detail", "Product", new { id = dto.ProductId });
    }
}
