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

    public ProductController(IProductService productService, ICategoryService categoryService,
        ISizeService sizeService, IBundleService bundleService,
        IProductCollectionService productCollectionService, IFavouriteService favService,
        IAccountService accountService, IReviewService reviewService, ICartItemService cartItemService)
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
    }

    public IActionResult Index()
    {
        return View();
    }
    public async Task<IActionResult> Detail(int id)
    {
        IDataResult<ProductGetDto> result = await _productService.GetByIdAsync(id, Includes.ProductIncludes);
        return View(result);
    }

    #region Other Actions
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

    [HttpPost]
    [Authorize(Roles = "SuperAdmin,Admin,User")]
    public async Task<IActionResult> WriteComment(ReviewPostDto dto)
    {
        dto.UserId = (await _accountService.GetUserByClaims(User)).Data.Id;
        IResult result = await _reviewService.CreateAsync(dto);
        return RedirectToAction("Detail", "Product", new {id=dto.ProductId});
    }

    [HttpPost]
    [Authorize(Roles = "SuperAdmin,Admin,User")]
    public async Task<IDataResult<CartItemGetDto>> AddItemToCart(int id)
    {
        ProductGetDto productDto = (await _productService.GetByIdAsync(id)).Data;
        var result = await _cartItemService.CreateAsync(productDto, (await _accountService.GetUserByClaims(User,Includes.UserIncludes)).Data);
        return result;
    }

    [HttpPost]
    [Authorize(Roles = "SuperAdmin,Admin,User")]
    public async Task<IDataResult<CartItemGetDto>> RemoveItemFromCart(int id, bool deleteAll = false)
    {
        ProductGetDto productDto = (await _productService.GetByIdAsync(id)).Data;
        var result = await _cartItemService.RemoveItemFromCartAsync(productDto, (await _accountService.GetUserByClaims(User, Includes.UserIncludes)).Data, deleteAll);
        return result;
    }
    #endregion
}
