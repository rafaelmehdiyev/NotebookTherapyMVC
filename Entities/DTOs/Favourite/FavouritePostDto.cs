namespace Entities.DTOs.Favourite;

public class FavouritePostDto : IDto
{
    public int ProductId { get; set; }
    public string UserId { get; set; }
}
