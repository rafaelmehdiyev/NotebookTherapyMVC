namespace Entities.Concrete;

public class ProductCollection : ITable
{
    public int Id { get; set; }
    public string CollectionIcon { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool isDeleted { get; set; }
    public DateTime CreatedDate { get; set; }
    //Relations
    public List<Product> Products { get; set; }
}
