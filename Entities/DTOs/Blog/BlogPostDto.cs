namespace Entities.DTOs.Blog;

public class BlogPostDto : IDto
{
    public IFormFile Image { get; set; }
    public string Title { get; set; }
    public string ShortDescription { get; set; }
    public string Content { get; set; }
}
