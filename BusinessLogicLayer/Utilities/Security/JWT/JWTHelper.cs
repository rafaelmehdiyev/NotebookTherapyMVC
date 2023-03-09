using BusinessLogicLayer.Utilities.Security.Encrption;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BusinessLogicLayer.Utilities.Security.JWT;

public class JWTHelper : ITokenHelper
{
    private readonly IConfiguration _configuration;
    private TokenOptions _tokenOption;
    private DateTime _accessTokenExp;

    public JWTHelper(IConfiguration configuration)
    {
        _configuration = configuration;
        _tokenOption = _configuration.GetSection("TokenOptions").Get<TokenOptions>();
        _accessTokenExp = DateTime.UtcNow.AddMinutes(_tokenOption.AccessTokenExpiration);
    }
    public AccessToken CreateToken(List<Claim> claims)
    {
        JwtHeader header = CreateJwtHeader(_tokenOption);
        JwtPayload payload = CreateJwtPayload(_tokenOption, claims);
        JwtSecurityToken securityToken = CreateJwtSecurityToken(header, payload);
        string token = new JwtSecurityTokenHandler().WriteToken(securityToken);
        return new AccessToken
        {
            Token = token,
            Expiration = _accessTokenExp
        };
    }
    private JwtHeader CreateJwtHeader(TokenOptions options)
    {
        SecurityKey key = SecurityKeyHelper.CreateSecurityKey(options.SecurityKey);
        SigningCredentials credentials = SigningCredentialsHelper.CreateSigningCredentials(key);
        return new JwtHeader(credentials);
    }
    private JwtPayload CreateJwtPayload(TokenOptions options, List<Claim> claims)
    {
        return new JwtPayload(options.Issuer, options.Auidence, claims, DateTime.UtcNow, _accessTokenExp);
    }
    private JwtSecurityToken CreateJwtSecurityToken(JwtHeader header, JwtPayload payload)
    {
        return new JwtSecurityToken(header, payload);
    }
}
