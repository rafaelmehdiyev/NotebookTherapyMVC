namespace NotebookTherapyMVC.Controllers;

public class ProductController : Controller
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    public IActionResult Index()
    {
        return View();
    }
    public async Task<IActionResult> Detail(int id)
    {
        IDataResult<ProductGetDto> result = await _productService.GetByIdAsync(id,Includes.ProductIncludes);
        return View(result);
    }
}
