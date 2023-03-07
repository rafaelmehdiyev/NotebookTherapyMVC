namespace Entities.DTOs.Favourite;

public class FavouriteGetDto : IDto
{
    public ProductGetDto Product { get; set; }
    public AppUser User { get; set; }
}