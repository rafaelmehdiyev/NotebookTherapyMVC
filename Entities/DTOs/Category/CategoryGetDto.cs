namespace Entities.DTOs.Category;

public class CategoryGetDto : IDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool isDeleted { get; set; }

    //Relations
    public List<ProductGetDto> Products { get; set; }
}