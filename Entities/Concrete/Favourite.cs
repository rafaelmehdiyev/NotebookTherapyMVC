namespace Entities.Concrete;

public class Favourite : ITable
{
    public int ProductId { get; set; }
    public Product Product { get; set; }
    public int UserId { get; set; }
    public AppUser User { get; set; }
}
