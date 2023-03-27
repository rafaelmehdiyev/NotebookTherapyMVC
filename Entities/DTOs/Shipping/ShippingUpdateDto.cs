namespace Entities.DTOs.Shipping;

public class ShippingUpdateDto : IDto
{
    public int Id { get; set; }
    public Country Country { get; set; }
    public string Address { get; set; }
    public string Apartment { get; set; }
    public string City { get; set; }
    public string? PhoneNumber { get; set; }
    public string? PostalCode { get; set; }

    //Relations
    public string UserId { get; set; }
}
