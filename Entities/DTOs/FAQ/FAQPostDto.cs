namespace Entities.DTOs.FAQ;

public class FAQPostDto : IDto
{
    public string Question { get; set; }
    public string Answer { get; set; }

    //Relations
    public int FAQCategoryId { get; set; }
}
