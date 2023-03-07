namespace Entities.DTOs.FAQCategory;

public class FAQCategoryGetDto : IDto
{
    public int Id { get; set; }
    public string Name { get; set; }

    //Relations
    public List<FAQGetDto> FAQs { get; set; }
}