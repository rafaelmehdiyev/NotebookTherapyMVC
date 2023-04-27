namespace NotebookTherapyMVC.Areas.Manage.Controllers;
[Area("Manage")]
[Authorize(Roles = "Admin,SuperAdmin")]
public class HomeController : Controller
{
    private readonly IProductService _productService;
    private readonly IAccountService _accountService;
    private readonly ISaleService _saleService;

    public HomeController(IProductService productService, IAccountService accountService, ISaleService saleService)
    {
        _productService = productService;
        _accountService = accountService;
        _saleService = saleService;
    }

    public async Task<IActionResult> Index()
    {
        IDataResult<List<ProductGetDto>> productResult = await _productService.GetAllAsync(false, Includes.ProductIncludes);
        IDataResult<List<UserGetDto>> userResult = await _accountService.GetAllUser(Includes.UserIncludes);
        IDataResult<List<SaleGetDto>> saleResult = await _saleService.GetAllAsync(false, Includes.SaleIncludes);
        ManageHomeVM vm = new()
        {
            ProductsResult = productResult.Data,
            UsersResult = userResult.Data,
            SalesResult = saleResult.Data
        };
        return View(vm);
    }
}

