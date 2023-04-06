namespace BusinessLogicLayer.Services.Abstract;

public interface IAccountService
{
    #region Auth Requests
    Task<IDataResult<UserGetDto>> Register(RegisterDto registerDto);
    Task<IDataResult<UserGetDto>> Login(LoginDto loginDto);
    Task<IResult> SignOutAsync();
    #endregion

    #region Get Requests
    Task<IDataResult<List<UserGetDto>>> GetAllUser(params string[] includes);
    Task<IDataResult<List<UserGetDto>>> GetAllUserByRole(string roleName,params string[] includes);
    Task<IDataResult<UserGetDto>> GetUserByClaims(ClaimsPrincipal userClaims,params string[] includes);
    Task<IDataResult<UserGetDto>> GetUserById(string id,params string[] includes);
    #endregion

    #region Update Requests
    Task<IResult> EvokeUserToAdmin(UserGetDto dto);
    Task<IResult> RevokeFromAdmin(UserGetDto dto);
    #endregion
}