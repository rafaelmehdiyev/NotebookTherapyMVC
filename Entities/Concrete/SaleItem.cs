namespace Entities.Concrete;

public class SaleItem : ITable
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Quantity { get; set; }
    public bool isDeleted { get; set; }
    public DateTime CreatedDate { get; set; }

    //Relations
    public int ProductId { get; set; }
    public Product Product { get; set; }
    public int CartId { get; set; }
    public Sale Sale { get; set; }
}
