namespace Entities.Concrete;

public class Review : ITable
{
    public int Id { get; set; }
    public string Comment { get; set; }
    public Rating Rating { get; set; }
    public bool isDeleted { get; set; }
    public DateTime CreatedDate { get; set; }

    //Relations
    public string UserId { get; set; }
    public AppUser User { get; set; }
    public int ProductId { get; set; }
    public Product Product { get; set; }
}