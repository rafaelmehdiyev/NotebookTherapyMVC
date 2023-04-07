namespace NotebookTherapyMVC.Controllers;

public class HomeController : Controller
{
    private readonly IProductService _productService;

    public HomeController(IProductService productService)
    {
        _productService = productService;
    }

    public async Task<IActionResult> Index()
    {
        IDataResult<List<ProductGetDto>> result = await _productService.GetAllAsync(false,Includes.ProductIncludes);
        return View(result);
    }
}
