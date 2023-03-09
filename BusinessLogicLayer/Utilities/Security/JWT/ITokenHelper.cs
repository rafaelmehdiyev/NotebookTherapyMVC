using System.Security.Claims;

namespace BusinessLogicLayer.Utilities.Security.JWT;

public interface ITokenHelper
{
    AccessToken CreateToken(List<Claim> claim);
}
