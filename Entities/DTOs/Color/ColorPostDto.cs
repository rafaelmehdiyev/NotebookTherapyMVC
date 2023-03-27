namespace Entities.DTOs.Color;

public class ColorPostDto : IDto
{
    public string Name { get; set; }
    public bool isDeleted { get; set; }
    public DateTime CreatedDate { get; set; }
}
