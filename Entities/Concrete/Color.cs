namespace Entities.Concrete;

public class Color : ITable
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool isDeleted { get; set; }
    public DateTime CreatedDate { get; set; }
    //Relations
    public List<Product> Products { get; set; }
}
