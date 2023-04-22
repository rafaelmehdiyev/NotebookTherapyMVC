using Entities.DTOs.SaleItem;
using x=NotebookTherapyMVC.Areas.Manage.Models;
namespace NotebookTherapyMVC.Areas.Manage.Controllers;
[Area("Manage")]
[Authorize(Roles = "Admin")]
public class HomeController : Controller
{
    private readonly IProductService _productService;
    private readonly IAccountService _accountService;
        private readonly ISaleItemService _saleItemService;

    public HomeController(IProductService productService, IAccountService accountService, ISaleItemService saleItemService)
    {
        _productService = productService;
        _accountService = accountService;
        _saleItemService = saleItemService;
    }

    public async Task<IActionResult> Index()
    {
        IDataResult<List<ProductGetDto>> productResult = await _productService.GetAllAsync(false, Includes.ProductIncludes);
        IDataResult<List<UserGetDto>> userResult = await _accountService.GetAllUser(Includes.UserIncludes);
        IDataResult<List<SaleItemGetDto>>saleItemResult= await _saleItemService.GetAllAsync(false);
        x.HomeVM vm = new()
        {
            ProductsResult = productResult,
            UsersResult = userResult,
            SaleItemsResult = saleItemResult
        };
        return View(vm);
    }
}

