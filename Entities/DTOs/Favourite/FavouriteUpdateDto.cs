﻿namespace Entities.DTOs.Favourite;

public class FavouriteUpdateDto : IDto
{
    public int ProductId { get; set; }
    public int UserId { get; set; }
}
