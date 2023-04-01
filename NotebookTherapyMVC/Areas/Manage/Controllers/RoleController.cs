namespace NotebookTherapyMVC.Areas.Manage.Controllers;
[Area("Manage")]
[Authorize(Roles = "Admin", AuthenticationSchemes ="AdminScheme")]
public class RoleController : Controller
{
    private readonly IRoleSevice _roleSevice;
    private readonly IMapper _mapper;

    public RoleController(IRoleSevice roleSevice, IMapper mapper)
    {
        _roleSevice = roleSevice;
        _mapper = mapper;
    }

    public async Task<IActionResult> Index()
    {
        var result = await _roleSevice.GetAllAsync();
        return View(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(RolePostDto dto)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }
        var result = await _roleSevice.CreateAsync(dto);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Update(string id)
    {
        var result = await _roleSevice.GetByIdAsync(id);
        RoleUpdateDto dto = _mapper.Map<RoleUpdateDto>(result.Data);
        return View(dto);
    }
    [HttpPost]
    public async Task<IActionResult> Update(RoleUpdateDto dto)
    {
        if (!ModelState.IsValid)
        {
            return View(dto);
        }
        await _roleSevice.UpdateAsync(dto);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(string id)
    {
        var result = (await _roleSevice.GetByIdAsync(id)).Data;
        if (result == null) { return RedirectToAction(nameof(Index)); }
        await _roleSevice.HardDeleteByIdAsync(id);
        return RedirectToAction(nameof(Index));
    }
}
