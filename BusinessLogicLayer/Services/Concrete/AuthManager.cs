namespace BusinessLogicLayer.Services.Concrete;

public class AuthManager : IAuthService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly ICartService _cartService;
    private readonly IMapper _mapper;

    public AuthManager(UserManager<AppUser> userManager, IMapper mapper, SignInManager<AppUser> signInManager, ICartService cartService)
    {
        _userManager = userManager;
        _mapper = mapper;
        _signInManager = signInManager;
        _cartService = cartService;
    }

    #region Auth Requests
    public async Task<IDataResult<UserGetDto>> Register(RegisterDto registerDto)
    {
        AppUser userForCheck = await _userManager.FindByNameAsync(registerDto.UserName);
        if (userForCheck is not null) { return new ErrorDataResult<UserGetDto>(_mapper.Map<UserGetDto>(userForCheck), "User Already Exist"); }
        AppUser user = _mapper.Map<AppUser>(registerDto);
        var result = await _userManager.CreateAsync(user, registerDto.Password);
        if (!result.Succeeded)
        {
            List<string> errors = new List<string>();
            foreach (var error in result.Errors) { errors.Add(error.Description); }
            return new ErrorDataResult<UserGetDto>(_mapper.Map<UserGetDto>(user), errors.ToArray());
        }
        await _userManager.AddClaimsAsync(user, new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier,user.Id),
            new Claim(ClaimTypes.Name,user.UserName)
        });
        await _userManager.AddToRoleAsync(user, "User");
        await _cartService.CreateAsync(new CartPostDto { UserId = user.Id });
        var cartResult = await _cartService.GetCartByUserIdAsync(user.Id);
        user.CartId = cartResult.Data.Id;
        await _userManager.UpdateAsync(user);
        return new SuccessDataResult<UserGetDto>(_mapper.Map<UserGetDto>(user), "Successfully Registered");
    }
    public async Task<IDataResult<UserGetDto>> Login(LoginDto loginDto)
    {
        AppUser user = await _userManager.FindByEmailAsync(loginDto.Email);
        if (user is null) { return new ErrorDataResult<UserGetDto>(_mapper.Map<UserGetDto>(user), "User is not exist"); }
        await _signInManager.SignInAsync(user, false);
        return new SuccessDataResult<UserGetDto>(_mapper.Map<UserGetDto>(user), "Login Succesful");
    }
    public async Task<IResult> SignOutAsync()
    {
        await _signInManager.SignOutAsync();
        return new SuccessResult();
    }
    #endregion

    #region Get Requests
    public async Task<IDataResult<UserGetDto>> GetUser(ClaimsPrincipal userClaims, params string[] includes)
    {
        AppUser user = await _userManager.GetUserAsync(userClaims);
        user = GetQuery(includes).FirstOrDefault();
        return new SuccessDataResult<UserGetDto>(_mapper.Map<UserGetDto>(user));
    }

    #endregion

    #region Private Methods
    private IQueryable<AppUser> GetQuery(string[] includes)
    {
        IQueryable<AppUser> query = _userManager.Users;
        if (includes is not null)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }
        return query;
    }
    #endregion
}
