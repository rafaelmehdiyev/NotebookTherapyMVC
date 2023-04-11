namespace NotebookTherapyMVC.Areas.Manage.Controllers;
[Area("Manage")]
//[Authorize(Roles = "Admin")]
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
        IDataResult<List<ProductCollectionGetDto>> result = await _productCollectionService.GetAllAsync(true);
        return View(result);
    }
    public async Task<IActionResult> Get(int id)
    {
        IDataResult<ProductCollectionGetDto> collection = await _productCollectionService.GetByIdAsync(id,Includes.ProductCollectionIncludes);
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
            return View(dto);
        }
        await _productCollectionService.CreateAsync(dto);
        return RedirectToAction("Index");
    }
    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        IDataResult<ProductCollectionGetDto> collection = await _productCollectionService.GetByIdAsync(id);
        ProductCollectionUpdateDto dto = _mapper.Map<ProductCollectionUpdateDto>(collection.Data);
        return View(dto);
    }
    [HttpPost]
    public async Task<IActionResult> Update(ProductCollectionUpdateDto dto)
    {
        if (!ModelState.IsValid)
        {
            return View(dto);
        }
        await _productCollectionService.UpdateAsync(dto);
        return RedirectToAction("Index");
    }
    public async Task<IActionResult> Delete(int id)
    {
        IDataResult<ProductCollectionGetDto> result = await _productCollectionService.GetByIdAsync(id);
        if (result is null)
        {
            return RedirectToAction(nameof(Index), result);
        }
        await _productCollectionService.SoftDeleteByIdAsync(result.Data.Id);
        return RedirectToAction(nameof(Index));
    }
	public async Task<IActionResult> Recover(int id)
	{
		IDataResult<ProductCollectionGetDto> result = await _productCollectionService.GetByIdAsync(id);
		if (result is null)
		{
			return RedirectToAction(nameof(Index), result);
		}
		await _productCollectionService.RecoverByIdAsync(result.Data.Id);
		return RedirectToAction(nameof(Index));
	}
	public async Task<IActionResult> HardDelete(int id)
	{
		IDataResult<ProductCollectionGetDto> result = await _productCollectionService.GetByIdAsync(id);
		if (result is null){return RedirectToAction(nameof(Index), result);}
		await _productCollectionService.HardDeleteByIdAsync(result.Data.Id);
		return RedirectToAction(nameof(Index));
	}
}
