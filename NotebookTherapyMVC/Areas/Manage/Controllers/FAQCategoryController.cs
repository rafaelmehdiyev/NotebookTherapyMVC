namespace NotebookTherapyMVC.Areas.Manage.Controllers;

[Area("Manage")]
[Authorize(Roles = "Admin")]
public class FAQCategoryController : Controller
{
	private readonly IFAQCategoryService _faqCategoryService;
	private readonly IMapper _mapper;

	public FAQCategoryController(IFAQCategoryService faqCategoryService, IMapper mapper)
	{
		_faqCategoryService = faqCategoryService;
		_mapper = mapper;
	}
	public async Task<IActionResult> Index()
	{
		IDataResult<List<FAQCategoryGetDto>> result = await _faqCategoryService.GetAllAsync(true, Includes.FAQCategoryIncludes);
		return View(result);
	}
	[HttpPost]
	public async Task<IActionResult> Create(FAQCategoryPostDto dto)
	{
		if (!ModelState.IsValid)
		{
			return View();
		}
		IResult result = await _faqCategoryService.CreateAsync(dto);
		return RedirectToAction(nameof(Index));
	}

	[HttpGet]
	public async Task<IActionResult> Update(int id)
	{
		IDataResult<FAQCategoryGetDto> result = await _faqCategoryService.GetByIdAsync(id);
		FAQCategoryUpdateDto dto = _mapper.Map<FAQCategoryUpdateDto>(result.Data);
		return View(dto);
	}
	[HttpPost]
	public async Task<IActionResult> Update(FAQCategoryUpdateDto dto)
	{
		if (!ModelState.IsValid)
		{
			return View(dto);
		}
		await _faqCategoryService.UpdateAsync(dto);
		return RedirectToAction(nameof(Index));
	}
	public async Task<IActionResult> Delete(int id)
	{
		FAQCategoryGetDto result = (await _faqCategoryService.GetByIdAsync(id)).Data;
		if (result == null) { return RedirectToAction(nameof(Index)); }
		await _faqCategoryService.SoftDeleteByIdAsync(id);
		return RedirectToAction(nameof(Index));
	}

	public async Task<IActionResult> Recover(int id)
	{
		FAQCategoryGetDto result = (await _faqCategoryService.GetByIdAsync(id)).Data;
		if (result == null) { return RedirectToAction(nameof(Index)); }
		await _faqCategoryService.RecoverByIdAsync(id);
		return RedirectToAction(nameof(Index));
	}
    public async Task<IActionResult> HardDelete(int id)
    {
        FAQCategoryGetDto result = (await _faqCategoryService.GetByIdAsync(id)).Data;
        if (result == null) { return RedirectToAction(nameof(Index)); }
        await _faqCategoryService.HardDeleteByIdAsync(id);
        return RedirectToAction(nameof(Index));
    }
}
