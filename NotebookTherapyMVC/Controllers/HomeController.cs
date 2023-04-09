namespace NotebookTherapyMVC.Controllers;

public class HomeController : Controller
{
    private readonly IProductService _productService;
    private readonly IBlogService _blogService;

    public HomeController(IProductService productService, IBlogService blogService)
    {
        _productService = productService;
        _blogService = blogService;
    }

    public async Task<IActionResult> Index()
    {
        IDataResult<List<ProductGetDto>> productResult = await _productService.GetAllAsync(false,Includes.ProductIncludes);
        IDataResult<List<BlogGetDto>> blogResult = await _blogService.GetAllAsync(false);
        HomeVM vm = new()
        {
            ProductsResult= productResult,
            BlogsResult= blogResult
        };
        return View(vm);
    }
}
