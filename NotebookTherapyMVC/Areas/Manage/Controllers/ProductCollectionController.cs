namespace NotebookTherapyMVC.Areas.Manage.Controllers;
[Area("Manage")]
[Authorize(Roles = "Admin")]
public class ProductCollectionController : Controller
{
    private readonly IProductCollectionService _productCollectionService;
    private readonly IMapper _mapper;

    public ProductCollectionController(IProductCollectionService productCollectionService, IMapper mapper)
    {
        _productCollectionService = productCollectionService;
        _mapper = mapper;
    }

    public async Task<IActionResult> Index()

    {
        var result = await _productCollectionService.GetAllAsync();
        return View(result);
    }
    public async Task<IActionResult> Get(int id)
    {
        var collection = await _productCollectionService.GetByIdAsync(id);
        return View(collection);
    }
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(ProductCollectionPostDto dto)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }
        await _productCollectionService.CreateAsync(dto);
        return RedirectToAction("Index");
    }
    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        var collection = await _productCollectionService.GetByIdAsync(id);
        ProductCollectionUpdateDto dto = _mapper.Map<ProductCollectionUpdateDto>(collection.Data);
        return View(dto);
    }
    [HttpPost]
    public async Task<IActionResult> Update(ProductCollectionUpdateDto dto)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }
        await _productCollectionService.UpdateAsync(dto);
        return RedirectToAction("Index");
    }
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _productCollectionService.GetByIdAsync(id);
        if (result is null)
        {
            return RedirectToAction(nameof(Index), result);
        }
        await _productCollectionService.SoftDeleteByIdAsync(result.Data.Id);
        return RedirectToAction(nameof(Index));
    }
	public async Task<IActionResult> Recover(int id)
	{
		var result = await _productCollectionService.GetByIdAsync(id);
		if (result is null)
		{
			return RedirectToAction(nameof(Index), result);
		}
		await _productCollectionService.RecoverByIdAsync(result.Data.Id);
		return RedirectToAction(nameof(Index));
	}
}
