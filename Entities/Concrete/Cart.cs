namespace Entities.Concrete;

public class Cart : ITable
{
    public int Id { get; set; }
    public bool isDeleted { get; set; }
    public DateTime CreatedDate { get; set; }

    //Relations
    public List<CartItem> CartItems { get; set; }
    public string UserId { get; set; }
    public AppUser User { get; set; }
}