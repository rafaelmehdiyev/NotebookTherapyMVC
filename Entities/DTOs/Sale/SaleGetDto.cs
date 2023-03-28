﻿namespace Entities.DTOs.Sale;

public class SaleGetDto : IDto
{
    public int Id { get; set; }
    public string SaleId { get; set; }
    public decimal TotalPrice { get; set; }
	public bool isDeleted { get; set; }
	//Relations
	public List<SaleItemGetDto> SaleItems { get; set; }
    public AppUser User { get; set; }
}