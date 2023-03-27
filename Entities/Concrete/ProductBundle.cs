namespace Entities.Concrete;

public class ProductBundle : ITable
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public Product Product { get; set; }
    public int BundleId { get; set; }
    public Bundle Bundle { get; set; }
}
