namespace Entities.Concrete;

public class Sale : ITable
{
    public Sale()
    {
        SaleId = Guid.NewGuid().ToString();
    }
    public int Id { get; set; }
    public string SaleId { get; set; }
    public decimal TotalPrice { get; set; }
    public SaleStatus SaleStatus { get; set; }
    public bool isDeleted { get; set; }
    public DateTime CreatedDate { get; set; }

    //Relations
    public List<SaleItem> SaleItems { get; set; }
    public string UserId { get; set; }
    public AppUser User { get; set; }
}

