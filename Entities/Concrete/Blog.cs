namespace Entities.Concrete;

public class Blog : ITable
{
    public int Id { get; set; }
    public string Content { get; set; }
    public int ViewCount { get; set; }
    public bool isDeleted { get; set; }
    public DateTime CreatedDate { get; set; }
}
