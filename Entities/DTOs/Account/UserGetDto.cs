namespace Entities.DTOs.Auth;

public class UserGetDto : IDto
{
    public UserGetDto()
    {
        Roles = new();
    }
    public string Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public List<string> Roles { get; set; }

    //Relations
    public CartGetDto? Cart { get; set; }
    public List<FavouriteGetDto> Favourites { get; set; }
    public List<ShippingGetDto> Shippings { get; set; }
    public List<SaleGetDto> Sales { get; set; }
    public List<ReviewGetDto> Reviews { get; set; }
}


