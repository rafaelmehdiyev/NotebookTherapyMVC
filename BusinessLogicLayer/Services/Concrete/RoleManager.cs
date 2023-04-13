using Entities.Abstract;

namespace BusinessLogicLayer.Services.Concrete;

public class RoleManager : IRoleSevice
{
    private readonly IMapper _mapper;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<AppUser> _userManager;

    public RoleManager(RoleManager<IdentityRole> roleManager, IMapper mapper, UserManager<AppUser> userManager)
    {
        _roleManager = roleManager;
        _mapper = mapper;
        _userManager = userManager;
    }
    #region Post Requests
    public async Task<IResult> CreateAsync(RolePostDto dto)
    {
        IdentityRole role = _mapper.Map<IdentityRole>(dto);
        IdentityResult result = await _roleManager.CreateAsync(role);
        if (!result.Succeeded)
        {
            return new ErrorResult(Messages.NotCreated(Messages.Role));
        }
        return new SuccessResult(Messages.Created(Messages.Role));
    }
    #endregion

    #region Get Requests
    public async Task<IDataResult<List<RoleGetDto>>> GetAllAsync()
    {
        List<IdentityRole> roles = await _roleManager.Roles.ToListAsync();
        if (roles is null)
        {
            return new ErrorDataResult<List<RoleGetDto>>(Messages.NotFound(Messages.Category));
        }
        List<RoleGetDto> result = _mapper.Map<List<RoleGetDto>>(roles);
        foreach (RoleGetDto dto in result)
        {
            dto.Users = _mapper.Map<List<UserGetDto>>(await _userManager.GetUsersInRoleAsync(dto.Name));
        }
        return new SuccessDataResult<List<RoleGetDto>>(result);
    }

    public async Task<IDataResult<RoleGetDto>> GetByIdAsync(string id)
    {
        IdentityRole role = await _roleManager.FindByIdAsync(id);
        
        if (role is null)
        {
            return new ErrorDataResult<RoleGetDto>(Messages.NotFound(Messages.Category));
        }
        RoleGetDto result = _mapper.Map<RoleGetDto>(role);
        result.Users = _mapper.Map<List<UserGetDto>>(await _userManager.GetUsersInRoleAsync(result.Name));
        return new SuccessDataResult<RoleGetDto>(result);
    }

    #endregion

    #region Update Requests
    public async Task<IResult> UpdateAsync(RoleUpdateDto dto)
    {
        IdentityRole role = await _roleManager.FindByIdAsync(dto.Id);
        role.Name = dto.Name;
        IdentityResult result = await _roleManager.UpdateAsync(role);
        if (!result.Succeeded)
        {
            return new ErrorResult(Messages.NotUpdated(Messages.Role));
        }
        return new SuccessResult(Messages.Updated(Messages.Role));
    }
    #endregion

    #region Delete Requests
    public async Task<IResult> HardDeleteByIdAsync(string id)
    {
        IdentityRole role = await _roleManager.FindByIdAsync(id);
        IdentityResult result = await _roleManager.DeleteAsync(role);
        if (!result.Succeeded)
        {
            return new ErrorResult(Messages.NotDeleted(Messages.Role));
        }
        return new SuccessResult(Messages.Deleted(Messages.Role));
    }

    #endregion
}
