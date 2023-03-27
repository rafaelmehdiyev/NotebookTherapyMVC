namespace Entities.DTOs.Blog;

public class BlogGetDto : IDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public int ViewCount { get; set; }
}
