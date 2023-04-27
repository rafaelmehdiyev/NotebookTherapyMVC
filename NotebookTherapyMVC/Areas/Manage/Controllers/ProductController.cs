namespace NotebookTherapyMVC.Areas.Manage.Controllers;
[Area("Manage")]
[Authorize(Roles = "Admin,SuperAdmin")]
public class ProductController : Controller
{
	private readonly IProductService _productService;
	private readonly ICategoryService _categoryService;
	private readonly ISizeService _sizeService;
	private readonly IBundleService _bundleService;
	private readonly IProductCollectionService _productCollectionService;
	private readonly IFavouriteService _favService;
	private readonly IAccountService _authService;
	private readonly IReviewService _reviewService;
	private readonly ICartItemService _cartItemService;
	private readonly IColorService _colorServoce;
	private readonly IMapper _mapper;

    public ProductController(IProductService service, IMapper mapper, ICategoryService categoryService, IProductCollectionService productCollectionService, ISizeService sizeService, IBundleService bundleService, IFavouriteService favService, IAccountService authService, IReviewService reviewService, ICartItemService cartItemService, IColorService colorServoce)
    {
        _productService = service;
        _mapper = mapper;
        _categoryService = categoryService;
        _productCollectionService = productCollectionService;
        _sizeService = sizeService;
        _bundleService = bundleService;
        _favService = favService;
        _authService = authService;
        _reviewService = reviewService;
        _cartItemService = cartItemService;
        _colorServoce = colorServoce;
    }

    public async Task<IActionResult> Index()
	{
        IDataResult<List<ProductGetDto>> result = await _productService.GetAllAsync(true, Includes.ProductIncludes);
		return View(result);
	}
	public async Task<IActionResult> Get(int id)
	{
		IDataResult<ProductGetDto> result = await _productService.GetByIdAsync(id,Includes.ProductIncludes);
		return PartialView("_getProductPartial", result);
	}
	[HttpGet]
	public async Task<IActionResult> Create()
	{
		await GetViewBags();
		return View();
	}

	[HttpPost]
	public async Task<IActionResult> Create(ProductPostDto dto)
	{
		if (!ModelState.IsValid)
		{
			await GetViewBags();
			return View(dto);
		}
		await _productService.CreateAsync(dto);
		return RedirectToAction(nameof(Index));
	}
	[HttpGet]
	public async Task<IActionResult> Update(int id)
	{
        IDataResult<ProductGetDto> result = await _productService.GetByIdAsync(id, "ProductImages", "Category", "ProductCollection", "ProductSizes.Size", "ProductBundles.Bundle");
		await GetViewBags();
		return View(_mapper.Map<ProductUpdateDto>(result.Data));
	}
	[HttpPost]
	public async Task<IActionResult> Update(ProductUpdateDto dto)
	{
		if (!ModelState.IsValid)
		{
			await GetViewBags();
			return View(dto);
		}
		await _productService.UpdateAsync(dto);
		return RedirectToAction(nameof(Index));
	}
	public async Task<IActionResult> Delete(int id)
	{
        IDataResult<ProductGetDto> result = await _productService.GetByIdAsync(id);
		if (result is null)
		{
			return RedirectToAction(nameof(Index), result);
		}
		await _productService.SoftDeleteByIdAsync(result.Data.Id);
		return RedirectToAction(nameof(Index));
	}

    public async Task<IActionResult> Recover(int id)
    {
        IDataResult<ProductGetDto> result = await _productService.GetByIdAsync(id);
        if (result is null)
        {
            return RedirectToAction(nameof(Index), result);
        }
        await _productService.RecoverByIdAsync(result.Data.Id);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> HardDelete(int id)
    {
        IDataResult<ProductGetDto> result = await _productService.GetByIdAsync(id);
        if (result is null)
        {
            return RedirectToAction(nameof(Index), result);
        }
        await _productService.HardDeleteByIdAsync(result.Data.Id);
        return RedirectToAction(nameof(Index));
    }
 //   #region Other Actions
 //   [HttpPost]
	//public async Task<IDataResult<FavouriteGetDto>> AddToFavourite(int id)
	//{
	//	FavouritePostDto dto = new()
	//	{
	//		ProductId = id,
	//		UserId = (await _authService.GetUserByClaims(User)).Data.Id
	//	};
	//	return await _favService.CreateAsync(dto);
	//}

	//[HttpPost]
	//public async Task<IResult> RemoveFromFavourite(int id)
	//{
	//	return await _favService.HardDeleteByIdAsync(id);
	//}

	//[HttpPost]
	//public async Task<IActionResult> WriteComment(ReviewPostDto dto)
	//{
	//	await _reviewService.CreateAsync(dto);
	//	return RedirectToAction("Get", "Product", new { id = dto.ProductId });
	//}

	//[HttpPost]
	//public async Task<IDataResult<CartItemGetDto>> AddItemToCart(int id)
	//{
	//	return await _cartItemService.CreateAsync(id, (await _authService.GetUserByClaims(User)).Data);
	//}

	//[HttpPost]
	//public async Task<IDataResult<CartItemGetDto>> RemoveItemFromCart(int id, bool deleteAll = false)
	//{
	//	return await _cartItemService.RemoveItemFromCartAsync(id, (await _authService.GetUserByClaims(User)).Data, deleteAll);
	//}
	//#endregion

	#region Private Methods
	private async Task GetViewBags()
	{
        IDataResult<List<CategoryGetDto>> categories = await _categoryService.GetAllAsync(false);
		IDataResult<List<ProductCollectionGetDto>> collections = await _productCollectionService.GetAllAsync(false);
		IDataResult<List<SizeGetDto>> sizes = await _sizeService.GetAllAsync(false);
		IDataResult<List<BundleGetDto>> bundles = await _bundleService.GetAllAsync(false);
		IDataResult<List<ColorGetDto>> colors = await _colorServoce.GetAllAsync(false);
        ViewBag.Categories = categories.Data.Select(c => new SelectListItem
		{
			Value = c.Id.ToString(),
			Text = c.Name
		});
		ViewBag.Collections = collections.Data.Select(c => new SelectListItem
		{
			Value = c.Id.ToString(),
			Text = c.Name
		});
		ViewBag.Sizes = sizes.Data.Select(c => new SelectListItem
		{
			Value = c.Id.ToString(),
			Text = c.Name
		});
		ViewBag.Bundles = bundles.Data.Select(c => new SelectListItem
		{
			Value = c.Id.ToString(),
			Text = c.Name
		});
		ViewBag.Colors = colors.Data.Select(c => new SelectListItem
        {
            Value = c.Id.ToString(),
            Text = c.Name
        });
    }
	#endregion
}
