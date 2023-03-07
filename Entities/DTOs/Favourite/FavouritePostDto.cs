namespace Entities.DTOs.Favourite;

public class FavouritePostDto : IDto
{
    public int ProductId { get; set; }
    public int UserId { get; set; }
}
