namespace Entities.DTOs.Blog;

public class BlogUpdateDto : IDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
}
