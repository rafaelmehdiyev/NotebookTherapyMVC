namespace Entities.DTOs.Blog;

public class BlogGetDto : IDto
{
    public int Id { get; set; }
    public string ImagePath { get; set; }
    public string Title { get; set; }
    public string ShortDescription { get; set; }
    public string Content { get; set; }
    public int ViewCount { get; set; }
    public bool isDeleted { get; set; }
    public DateTime CreatedDate { get; set; }
}
