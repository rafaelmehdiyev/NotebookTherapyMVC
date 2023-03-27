namespace Entities.DTOs.Size;

public class SizeGetDto : IDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    //Relations
    public List<ProductSizeGetDto> ProductSizes { get; set; }
}