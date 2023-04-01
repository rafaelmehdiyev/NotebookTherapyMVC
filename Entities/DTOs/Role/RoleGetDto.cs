namespace Entities.DTOs.Role;

public class RoleGetDto : IDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public int? UserCount { get; set; }
}