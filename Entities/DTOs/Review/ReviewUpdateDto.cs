namespace Entities.DTOs.Review;

public class ReviewUpdateDto : IDto
{
    public int Id { get; set; }
    public string Comment { get; set; }
    public Rating Rating { get; set; }

    //Relations
    public int UserId { get; set; }
    public int ProductId { get; set; }
}