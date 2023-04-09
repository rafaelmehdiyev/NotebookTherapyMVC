namespace NotebookTherapyMVC.Controllers;

public class BlogController : Controller
{
    private readonly IBlogService _blogService;

    public BlogController(IBlogService blogService)
    {
        _blogService = blogService;
    }

    public async Task<IActionResult> Index()
    {
        IDataResult<List<BlogGetDto>> result = await _blogService.GetAllAsync(false);
        return View(result);
    }
    public async Task<IActionResult> Detail(int id)
    {
        IDataResult<BlogGetDto> result = await _blogService.GetByIdAsync(id);
        return View(result);
    }
    public async Task<IActionResult> GetPaginate(int page,int size)
    {
        IDataResult<List<BlogGetDto>> result = await _blogService.GetAllPaginateAsync(page,size,false);
        return PartialView("_bloglistPartial", result.Data);
    }
    public async Task<IActionResult> GetRelated()
    {
        IDataResult<List<BlogGetDto>> result = await _blogService.GetAllAsync(false);
        List<BlogGetDto> related = result.Data.OrderBy(x => Guid.NewGuid()).Take(3).ToList();
        return PartialView("_blogRelatedPartial", related);
    }
}
