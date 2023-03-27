namespace Entities.DTOs.Color;

public class ColorUpdateDto : IDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool isDeleted { get; set; }
    public DateTime CreatedDate { get; set; }
}
