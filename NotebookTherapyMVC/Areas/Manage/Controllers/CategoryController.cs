namespace NotebookTherapyMVC.Areas.Manage.Controllers;

[Area("Manage")]
public class CategoryController : Controller
{
    private readonly ICategoryService _categoryService;
    private readonly IMapper _mapper;

	public CategoryController(ICategoryService categoryService, IMapper mapper)
	{
		_categoryService = categoryService;
		_mapper = mapper;
	}
	public async Task<IActionResult> Index()
    {
        var result = await _categoryService.GetAllAsync();
        return View(result);
    }
    [HttpPost]
	public async Task<IActionResult> Create(CategoryPostDto dto)
	{
        if (!ModelState.IsValid)
        {         
            return View();
        }
        var result = await _categoryService.CreateAsync(dto);
		return RedirectToAction(nameof(Index));
	}

    [HttpGet]
	public async Task<IActionResult> Update(int id)
	{
		var result = await _categoryService.GetByIdAsync(id);
        CategoryUpdateDto dto = _mapper.Map<CategoryUpdateDto>(result.Data);
		return View(dto);
	}
	[HttpPost]
	public async Task<IActionResult> Update(CategoryUpdateDto dto)
	{
		if (!ModelState.IsValid)
		{
			return View(dto);
		}
		await _categoryService.UpdateAsync(dto);
		return RedirectToAction(nameof(Index));
	}
	public async Task<IActionResult> Delete(int id)
	{
		var result = (await _categoryService.GetByIdAsync(id)).Data;
		if (result == null) { return RedirectToAction(nameof(Index)); }
		await _categoryService.SoftDeleteByIdAsync(id);
		return RedirectToAction(nameof(Index));
	}

	public async Task<IActionResult> Recover(int id)
	{
		var result = (await _categoryService.GetByIdAsync(id)).Data;
		if (result == null) { return RedirectToAction(nameof(Index)); }
		await _categoryService.RecoverByIdAsync(id);
		return RedirectToAction(nameof(Index));
	}
}
