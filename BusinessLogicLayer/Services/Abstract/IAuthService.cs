namespace BusinessLogicLayer.Services.Abstract;

public interface IAuthService
{
    #region Auth Requests
    Task<IDataResult<UserGetDto>> Register(RegisterDto registerDto);
    Task<IDataResult<UserGetDto>> Login(LoginDto loginDto);
    Task<IResult> SignOutAsync();
    #endregion

    #region Get Requests
    Task<IDataResult<UserGetDto>> GetUser(ClaimsPrincipal userClaims,params string[] includes);
    #endregion
}