namespace Entities.Concrete;

public class ProductImage : ITable
{
    public int Id { get; set; }
    public string ImagePath { get; set; }
    public bool isDeleted { get; set; }
    public DateTime CreatedDate { get; set; }

    //Relations
    public int ProductId { get; set; }
    public Product Product { get; set; }
}
