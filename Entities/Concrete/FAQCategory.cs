namespace Entities.Concrete;

public class FAQCategory : ITable
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool isDeleted { get; set; }
    public DateTime CreatedDate { get; set; }

    //Relations
    public List<FAQ> FAQs { get; set; }
}
