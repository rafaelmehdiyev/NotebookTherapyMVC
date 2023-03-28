namespace Entities.DTOs.Color;

public class ColorUpdateDto : IDto
{
    public int Id { get; set; }
    public string ColorCode { get; set; }
}
