namespace NotebookTherapyMVC.Areas.Manage.Controllers;
[Area("Manage")]
public class UserController : Controller
{
    private readonly IAccountService _accountService;

    public UserController(IAccountService accountService)
    {
        _accountService = accountService;
    }


    [HttpGet]
    public async Task<IActionResult> Get(string id)
    {
        IDataResult<UserGetDto> result = await _accountService.GetUserById(id);
        return View(result);
    } 
    public async Task<IActionResult> Index()
    {
        IDataResult<List<UserGetDto>> users = await _accountService.GetAllUser();
        return View(users);
    }
    public async Task<IActionResult> EvokeToAdmin(string id)
    {
        UserGetDto user = (await _accountService.GetUserById(id)).Data;
        await _accountService.EvokeUserToAdmin(user);
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> RevokeFromAdmin(string id)
    {
        UserGetDto user = (await _accountService.GetUserById(id)).Data;
        await _accountService.RevokeFromAdmin(user);
        return RedirectToAction(nameof(Index));
    }
}
