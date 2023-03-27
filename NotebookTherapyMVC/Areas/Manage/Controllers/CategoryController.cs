namespace NotebookTherapyMVC.Areas.Manage.Controllers;

[Area("Manage")]
public class CategoryController : Controller
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
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
}
