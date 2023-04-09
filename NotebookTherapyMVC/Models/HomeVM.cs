namespace NotebookTherapyMVC.Models;

public class HomeVM
{
    public IDataResult<List<ProductGetDto>>? ProductsResult { get; set; }
    public IDataResult<ProductGetDto>? ProductResult { get; set; }
    public IDataResult<List<BlogGetDto>>? BlogsResult { get; set; }
    public IDataResult<BlogGetDto>? BlogResult { get; set; }
}
