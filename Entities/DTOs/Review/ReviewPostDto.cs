namespace Entities.DTOs.Review;

public class ReviewPostDto : IDto
{
    public string Comment { get; set; }
    public Rating Rating { get; set; }

    //Relations
    public int UserId { get; set; }
    public int ProductId { get; set; }
}
