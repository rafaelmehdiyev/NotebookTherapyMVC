namespace NotebookTherapyMVC.Controllers;

public class HomeController : Controller
{
    private readonly IProductService _productService;
    private readonly IBlogService _blogService;
    private readonly IFAQCategoryService _faqCategoryService;
    private readonly IFAQService _faqService;


    public HomeController(IProductService productService, IBlogService blogService, IFAQCategoryService faqCategoryService, IFAQService faqService)
    {
        _productService = productService;
        _blogService = blogService;
        _faqCategoryService = faqCategoryService;
        _faqService = faqService;
    }

    public async Task<IActionResult> Index()
    {
        IDataResult<List<ProductGetDto>> productResult = await _productService.GetAllAsync(false, Includes.ProductIncludes);
        IDataResult<List<BlogGetDto>> blogResult = await _blogService.GetAllAsync(false);
        HomeVM vm = new()
        {
            ProductsResult = productResult,
            BlogsResult = blogResult
        };
        return View(vm);
    }

    public IActionResult About()
    {
        return View();
    }
    public async Task<IActionResult> Contact()
    {
        await GetFAQCategories();
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> CreateFAQ(FAQPostDto dto)
    {
        if (dto.Question != null && dto.FAQCategoryId != null)
        {
            await _faqService.CreateAsync(dto);
        }
        return RedirectToAction(nameof(Contact));
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
