namespace NotebookTherapyMVC.Areas.Manage.Controllers;

[Area("Manage")]
[Authorize(Roles = "Admin,SuperAdmin")]
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
        IDataResult<List<SizeGetDto>> result = await _sizeService.GetAllAsync(true);
        return View(result);
    }

    [HttpGet]
    public async Task<IActionResult> Get(int id)
    {
        IDataResult<SizeGetDto> result = await _sizeService.GetByIdAsync(id,Includes.SizeIncludes);
        return View(result);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        return View();
    }




    [HttpPost]
    public async Task<IActionResult> Create(SizePostDto dto)
    {
        if (!ModelState.IsValid)
        {
            return View(dto);
        }
        IResult result = await _sizeService.CreateAsync(dto);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        IDataResult<SizeGetDto> result = await _sizeService.GetByIdAsync(id);
        return View(_mapper.Map<SizeUpdateDto>(result.Data));
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
        SizeGetDto result = (await _sizeService.GetByIdAsync(id)).Data;
        if (result == null) { return RedirectToAction(nameof(Index)); }
        await _sizeService.SoftDeleteByIdAsync(id);
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Recover(int id)
    {
        SizeGetDto result = (await _sizeService.GetByIdAsync(id)).Data;
        if (result == null) { return RedirectToAction(nameof(Index)); }
        await _sizeService.RecoverByIdAsync(id);
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> HardDelete(int id)
    {
        SizeGetDto result = (await _sizeService.GetByIdAsync(id)).Data;
        if (result == null) { return RedirectToAction(nameof(Index)); }
        await _sizeService.HardDeleteByIdAsync(id);
        return RedirectToAction(nameof(Index));
    }
}
