namespace NotebookTherapyMVC.Areas.Manage.Controllers;
[Area("Manage")]
//[Authorize(Roles = "Admin")]
public class BlogController : Controller
{
    private readonly IBlogService _blogService;
    private readonly IMapper _mapper;

    public BlogController(IBlogService blogService, IMapper mapper)
    {
        _blogService = blogService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> Get(int id)
    {
        IDataResult<BlogGetDto> result = await _blogService.GetByIdAsync(id);

        return PartialView("_getBlogPartial", result);

    }
  

    public async Task<IActionResult> Index()
    {
        IDataResult<List<BlogGetDto>> result = await _blogService.GetAllAsync(true);
        return View(result);
    }
    [HttpGet]
    public async Task<IActionResult> Create()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(BlogPostDto dto)
    {
        if (!ModelState.IsValid)
        {
            return View(dto);
        }
        await _blogService.CreateAsync(dto);
        return RedirectToAction(nameof(Index));
    }
    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        IDataResult<BlogGetDto> result = await _blogService.GetByIdAsync(id);
        BlogUpdateDto dto = _mapper.Map<BlogUpdateDto>(result.Data);
        return View(dto);
    }
    [HttpPost]
    public async Task<IActionResult> Update(BlogUpdateDto dto)
    {
        if (!ModelState.IsValid)
        {
            return View(dto);
        }
        await _blogService.UpdateAsync(dto);
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Delete(int id)
    {
        BlogGetDto result = (await _blogService.GetByIdAsync(id)).Data;
        if (result == null) { return RedirectToAction(nameof(Index)); }
        await _blogService.SoftDeleteByIdAsync(id);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Recover(int id)
    {
        BlogGetDto result = (await _blogService.GetByIdAsync(id)).Data;
        if (result == null) { return RedirectToAction(nameof(Index)); }
        await _blogService.RecoverByIdAsync(id);
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> HardDelete(int id)
    {
        BlogGetDto result = (await _blogService.GetByIdAsync(id)).Data;
        if (result == null) { return RedirectToAction(nameof(Index)); }
        await _blogService.HardDeleteByIdAsync(id);
        return RedirectToAction(nameof(Index));
    }
}
