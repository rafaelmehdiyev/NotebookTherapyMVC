namespace NotebookTherapyMVC.Areas.Manage.Controllers;

[Area("Manage")]
public class SizeController : Controller
{
	private readonly ISizeService _sizeService;
	private readonly IMapper _mapper;

	public SizeController(ISizeService sizeService, IMapper mapper)
	{
		_sizeService = sizeService;
		_mapper = mapper;
	}
	public async Task<IActionResult> Index()
	{
		var result = await _sizeService.GetAllAsync();
		return View(result);
	}
	[HttpPost]
	public async Task<IActionResult> Create(SizePostDto dto)
	{
		if (!ModelState.IsValid)
		{
			return View();
		}
		var result = await _sizeService.CreateAsync(dto);
		return RedirectToAction(nameof(Index));
	}

	[HttpGet]
	public async Task<IActionResult> Update(int id)
	{
		var result = await _sizeService.GetByIdAsync(id);
		SizeUpdateDto dto = _mapper.Map<SizeUpdateDto>(result.Data);
		return View(dto);
	}
	[HttpPost]
	public async Task<IActionResult> Update(SizeUpdateDto dto)
	{
		if (!ModelState.IsValid)
		{
			return View(dto);
		}
		await _sizeService.UpdateAsync(dto);
		return RedirectToAction(nameof(Index));
	}
	public async Task<IActionResult> Delete(int id)
	{
		var result = (await _sizeService.GetByIdAsync(id)).Data;
		if (result == null) { return RedirectToAction(nameof(Index)); }
		await _sizeService.SoftDeleteByIdAsync(id);
		return RedirectToAction(nameof(Index));
	}
	public async Task<IActionResult> Recover(int id)
	{
		var result = (await _sizeService.GetByIdAsync(id)).Data;
		if (result == null) { return RedirectToAction(nameof(Index)); }
		await _sizeService.RecoverByIdAsync(id);
		return RedirectToAction(nameof(Index));
	}
}
