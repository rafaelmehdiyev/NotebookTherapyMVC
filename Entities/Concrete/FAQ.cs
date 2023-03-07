namespace Entities.Concrete;

public class FAQ : ITable
{
    public int Id { get; set; }
    public string Question { get; set; }
    public string Answer { get; set; }
    public bool isAnswered { get; set; }
    public bool isHidden { get; set; }
    public bool isDeleted { get; set; }
    public DateTime CreatedDate { get; set; }

    //Relations
    public int FAQCategoryId { get; set; }
    public FAQCategory FAQCategory { get; set; }
}
