namespace BusinessLogicLayer.Services.Concrete;

public class AccountManager : IAccountService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly ICartService _cartService;
    private readonly IMapper _mapper;

    public AccountManager(UserManager<AppUser> userManager,
        IMapper mapper, SignInManager<AppUser> signInManager,
        ICartService cartService)
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
        IdentityResult result = await _userManager.CreateAsync(user, registerDto.Password);
        if (!result.Succeeded)
        {
            List<string> errors = new List<string>();
            foreach (IdentityError error in result.Errors) { errors.Add(error.Description); }
            return new ErrorDataResult<UserGetDto>(_mapper.Map<UserGetDto>(user), errors.ToArray());
        }
        await _userManager.AddClaimsAsync(user, new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier,user.Id),
            new Claim(ClaimTypes.Name,user.UserName)
        });
        await _userManager.AddToRoleAsync(user, "User");
        await _cartService.CreateAsync(new CartPostDto { UserId = user.Id });
        IDataResult<CartGetDto> cartResult = await _cartService.GetCartByUserIdAsync(user.Id);
        user.CartId = cartResult.Data.Id;
        await _userManager.UpdateAsync(user);
        UserGetDto userDto = _mapper.Map<UserGetDto>(user);
        userDto.Roles = (List<string>)await _userManager.GetRolesAsync(user);
        return new SuccessDataResult<UserGetDto>(userDto, "Successfully Registered");
    }
    public async Task<IDataResult<UserGetDto>> Login(LoginDto loginDto)
    {
        AppUser user = await _userManager.FindByEmailAsync(loginDto.Email);
        if (user is null) { return new ErrorDataResult<UserGetDto>(_mapper.Map<UserGetDto>(user), "User is not exist"); }
        await _signInManager.SignInAsync(user, false);
        UserGetDto userDto = _mapper.Map<UserGetDto>(user);
        userDto.Roles = (List<string>)await _userManager.GetRolesAsync(user);
        return new SuccessDataResult<UserGetDto>(userDto, "Login Succesful");
    }
    public async Task<IResult> SignOutAsync()
    {
        await _signInManager.SignOutAsync();
        return new SuccessResult();
    }
    #endregion

    #region Get Requests
    public async Task<IDataResult<List<UserGetDto>>> GetAllUser(params string[] includes)
    {
        List<AppUser> userList = GetQuery(includes).ToList();
		return new SuccessDataResult<List<UserGetDto>>(await GetUserDtos(userList));
    }
    public async Task<IDataResult<List<UserGetDto>>> GetAllUserByRole(string roleName,params string[] includes)
    {
        IList<AppUser> userList = await _userManager.GetUsersInRoleAsync(roleName);
        userList = GetQuery(includes).ToList();
        return new SuccessDataResult<List<UserGetDto>>(_mapper.Map<List<UserGetDto>>(userList));
    }
    public async Task<IDataResult<UserGetDto>> GetUserByClaims(ClaimsPrincipal userClaims, params string[] includes)
    {
        List<AppUser> userList = GetQuery(includes).ToList();
        AppUser user = await _userManager.GetUserAsync(userClaims);
        return new SuccessDataResult<UserGetDto>(_mapper.Map<UserGetDto>(userList.Where(u=>u.Id == user.Id).FirstOrDefault()));
    }

    public async Task<IDataResult<UserGetDto>> GetUserById(string id, params string[] includes)
    {
        List<AppUser> userList = GetQuery(includes).ToList();
        return new SuccessDataResult<UserGetDto>(_mapper.Map<UserGetDto>(userList.Where(u=>u.Id == id).FirstOrDefault()));
    }
    #endregion

    #region Update Requests
    public async Task<IResult> EvokeUserToAdmin(UserGetDto dto)
    {
        AppUser user = await _userManager.FindByIdAsync(dto.Id);
        await _userManager.RemoveFromRoleAsync(user, "User");
        IdentityResult result = await _userManager.AddToRoleAsync(user, "Admin");
        if (!result.Succeeded)
        {
            return new ErrorResult("The user could not become an admin");
        }
        return new ErrorResult("The user is an admin now");
    }

    public async Task<IResult> RevokeFromAdmin(UserGetDto dto)
    {
        AppUser user = await _userManager.FindByIdAsync(dto.Id);
        await _userManager.RemoveFromRoleAsync(user, "Admin");
        IdentityResult result = await _userManager.AddToRoleAsync(user, "User");
        if (!result.Succeeded)
        {
            return new ErrorResult("Can't revoke the admin");
        }
        return new ErrorResult("The admin successfully revoked");
    }
    #endregion

    #region Private Methods
    private IQueryable<AppUser> GetQuery(string[] includes)
    {
        IQueryable<AppUser> query = _userManager.Users;
        if (includes is not null)
        {
            foreach (string include in includes)
            {
                query = query.Include(include);
            }
        }
        return query;
    }
    private async Task<List<UserGetDto>> GetUserDtos(List<AppUser> userList)
    {
        List<UserGetDto> users = _mapper.Map<List<UserGetDto>>(userList);
        for (int i = 0; i < userList.Count; i++)
        {
            for (int j = 0; j < users.Count; j++)
            {
                users[i].Roles = (List<string>)await _userManager.GetRolesAsync(userList[i]);
            }
        }
        return users;
    } 
    #endregion
}
