using Entities.DTOs.SaleItem;

namespace NotebookTherapyMVC.Areas.Manage.Models;

public class HomeVM
{
    public IDataResult<List<ProductGetDto>>? ProductsResult { get; set; }
    public IDataResult<List<UserGetDto>>? UsersResult { get; set; }
    public IDataResult<List<SaleItemGetDto>>? SaleItemsResult { get; set; }
}

