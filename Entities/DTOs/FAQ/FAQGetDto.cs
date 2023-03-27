namespace Entities.DTOs.FAQ;

public class FAQGetDto : IDto
{
    public int Id { get; set; }
    public string Question { get; set; }
    public string Answer { get; set; }
    public bool isAnswered { get; set; }

    //Relations
    public FAQCategoryGetDto FAQCategory { get; set; }
}