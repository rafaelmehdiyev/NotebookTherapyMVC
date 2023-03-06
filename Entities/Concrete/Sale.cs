namespace Entities.Concrete;

public class Sale : ITable
{
    public int Id { get; set; }
    public bool isDeleted { get; set; }
    public DateTime CreatedDate { get; set; }

    //Relations
    public List<SaleItem> SaleItems { get; set; }
    public int UserId { get; set; }
    public AppUser User { get; set; }
}
