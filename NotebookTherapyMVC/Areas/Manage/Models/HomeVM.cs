namespace NotebookTherapyMVC.Areas.Manage.Models;

public class ManageHomeVM
{
    public List<ProductGetDto>? ProductsResult { get; set; }
    public List<UserGetDto>? UsersResult { get; set; }
    public List<SaleGetDto>? SalesResult { get; set; }
}

