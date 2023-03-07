namespace Entities.DTOs.Sale;

public class SaleGetDto : IDto
{
    public int Id { get; set; }
    //Relations
    public List<SaleItemGetDto> SaleItems { get; set; }
    public AppUser User { get; set; }
}