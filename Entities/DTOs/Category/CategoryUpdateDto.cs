﻿namespace Entities.DTOs.Category;

public class CategoryUpdateDto : IDto
{
    public int Id { get; set; }
    public string Name { get; set; }
}