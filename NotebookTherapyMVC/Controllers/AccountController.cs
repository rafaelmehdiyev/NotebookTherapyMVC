namespace NotebookTherapyMVC.Controllers;

public class AccountController : Controller
{
    private readonly IAccountService _accountService;
    private readonly ICartService _cartService;

    public AccountController(IAccountService accountService, ICartService cartService)
    {
        _accountService = accountService;
        _cartService = cartService;
    }

    [HttpGet]
    public async Task<IActionResult> Register()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Register(RegisterDto registerDto)
    {
        IDataResult<UserGetDto> result = await _accountService.Register(registerDto);
        if (!result.Success)
        {
            return BadRequest(result);
        }
        return RedirectToAction(nameof(Login));
    }
    [HttpGet]
    public async Task<IActionResult> Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginDto loginDto)
    {
        IDataResult<UserGetDto> result = await _accountService.Login(loginDto);
        if (!result.Success) { return View(result); }
        if (result.Data.Roles.Contains("Admin"))
        {
            return RedirectToAction("Index", "Product", new { area = "Manage" });
        }
        else
        {
            return RedirectToAction("Index", "Home");
        }
    }
    [Authorize(Roles = "SuperAdmin,Admin,User")]
    public async Task<IActionResult> Signout()
    {
        await _accountService.SignOutAsync();
        return RedirectToAction(nameof(Login));
    }
    [Authorize(Roles = "SuperAdmin,Admin,User")]
    public async Task<IActionResult> Profile()
    {
        IDataResult<UserGetDto> result = await _accountService.GetUserByClaims(User, Includes.UserIncludes);
        return View(result);
    }

    [Authorize(Roles = "SuperAdmin,Admin,User")]
    public async Task<IActionResult> ShoppingCart()
    {
        IDataResult<UserGetDto> userResult = await _accountService.GetUserByClaims(User, Includes.UserIncludes);
        IDataResult<CartGetDto> cartResult = await _cartService.GetByIdAsync(userResult.Data.Cart.Id,Includes.CartIncludes);
        return View(cartResult);
    }
    public async Task<IActionResult> LoadCartItems(int id)
    {
        IDataResult<CartGetDto> cartResult = await _cartService.GetByIdAsync(id, Includes.CartIncludes);
        return PartialView("_cartItemPartial", cartResult.Data.CartItems.Where(c=>!c.isDeleted).ToList());
    }
}
