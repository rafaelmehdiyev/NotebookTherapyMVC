namespace Entities.DTOs.Favourite;

public class FavouriteGetDto : IDto
{
    public int Id { get; set; }
    public ProductGetDto Product { get; set; }
    public UserGetDto User { get; set; }
}