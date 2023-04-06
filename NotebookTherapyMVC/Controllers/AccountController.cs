namespace NotebookTherapyMVC.Controllers
{ 
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
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
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> Signout()
        {
            await _accountService.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> Profile()
        {
            IDataResult<UserGetDto> result = await _accountService.GetUserByClaims(User, Includes.UserIncludes);
            return View(result);
        }
    }
}
