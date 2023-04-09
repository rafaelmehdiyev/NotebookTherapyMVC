namespace Entities.DTOs.Blog;

public class BlogUpdateDto : IDto
{
    public int Id { get; set; }
    public IFormFile Image { get; set; }
    public string Title { get; set; }
    public string ShortDescription { get; set; }
    public string Content { get; set; }
}
