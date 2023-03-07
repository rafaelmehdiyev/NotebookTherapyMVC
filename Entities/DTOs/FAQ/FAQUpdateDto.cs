namespace Entities.DTOs.FAQ;

public class FAQUpdateDto : IDto
{
    public int Id { get; set; }
    public string Question { get; set; }
    public string Answer { get; set; }

    //Relations
    public int FAQCategoryId { get; set; }
}
