namespace NotebookTherapyMVC.Areas.Manage.Controllers
{
	[Area("Manage")]
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
			var result = await _accountService.Register(registerDto);
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
			var result = await _accountService.Login(loginDto);
			if (!result.Success) { return View(result); }
			return RedirectToAction("Index", "Product", new {area = "Manage"});
		}
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Signout()
		{
			await _accountService.SignOutAsync();
			return RedirectToAction(nameof(Login));
		}
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Profile()
		{
			var result = await _accountService.GetUser(User, Includes.UserIncludes);
			return View(result);
		}
	}
}
