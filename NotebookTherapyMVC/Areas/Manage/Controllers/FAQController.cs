namespace NotebookTherapyMVC.Areas.Manage.Controllers;
[Area("Manage")]
[Authorize(Roles = "Admin")]
public class FAQController : Controller
{
    private readonly IFAQCategoryService _faqCategoryService;
    private readonly IFAQService _faqService;
    private readonly IMapper _mapper;

    public FAQController(IFAQService faqService, IMapper mapper, IFAQCategoryService faqCategoryService)
    {
        _faqService = faqService;
        _mapper = mapper;
        _faqCategoryService = faqCategoryService;
    }

    public async Task<IActionResult> Index()
    {
        IDataResult<List<FAQGetDto>> result = await _faqService.GetAllAsync(true, Includes.FAQIncludes);
        return View(result);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        await GetFAQCategories();
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(FAQPostDto dto)
    {
        if (!ModelState.IsValid)
        {
            await GetFAQCategories();
            return View();
        }
        await _faqService.CreateAsync(dto);
        return RedirectToAction(nameof(Index));
    }

	[HttpGet]
	public async Task<IActionResult> Update(int id)
	{
        IDataResult<FAQGetDto> result = await _faqService.GetByIdAsync(id);
        await GetFAQCategories();
        FAQUpdateDto dto = _mapper.Map<FAQUpdateDto>(result.Data);
		return View(dto);
	}

	[HttpPost]
	public async Task<IActionResult> Update(FAQUpdateDto dto)
	{
		if (!ModelState.IsValid)
		{
			await GetFAQCategories();
			return View();
		}
		await _faqService.UpdateAsync(dto);
		return RedirectToAction(nameof(Index));
	}

	public async Task<IActionResult> Delete(int id)
	{
		FAQGetDto result = (await _faqService.GetByIdAsync(id)).Data;
		if (result == null) { return RedirectToAction(nameof(Index)); }
		await _faqService.SoftDeleteByIdAsync(id);
		return RedirectToAction(nameof(Index));
	}

	public async Task<IActionResult> Recover(int id)
	{
		FAQGetDto result = (await _faqService.GetByIdAsync(id)).Data;
		if (result == null) { return RedirectToAction(nameof(Index)); }
		await _faqService.RecoverByIdAsync(id);
		return RedirectToAction(nameof(Index));
	}

    public async Task<IActionResult> HardDelete(int id)
    {
        FAQGetDto result = (await _faqService.GetByIdAsync(id)).Data;
        if (result == null) { return RedirectToAction(nameof(Index)); }
        await _faqService.HardDeleteByIdAsync(id);
        return RedirectToAction(nameof(Index));
    }
    #region Private Methods
    private async Task GetFAQCategories()
    {
        IDataResult<List<FAQCategoryGetDto>> result = await _faqCategoryService.GetAllAsync(false);
        IEnumerable<SelectListItem> faqCategories = result.Data.Select(c => new SelectListItem
        {
            Value = c.Id.ToString(),
            Text = c.Name
        });
        ViewBag.FAQCategories = faqCategories;
    }
    #endregion
}
