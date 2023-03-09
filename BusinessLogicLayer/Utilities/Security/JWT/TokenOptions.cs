namespace BusinessLogicLayer.Utilities.Security.JWT;

public class TokenOptions
{
    public string Issuer { get; set; }
    public string Auidence { get; set; }
    public int AccessTokenExpiration { get; set; }
    public string SecurityKey { get; set; }
}