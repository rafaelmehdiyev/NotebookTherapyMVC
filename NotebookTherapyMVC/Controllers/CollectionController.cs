namespace NotebookTherapyMVC.Controllers;

public class CollectionController : Controller
{
    private readonly IProductCollectionService _productCollectionService;

    public CollectionController(IProductCollectionService productCollectionService)
    {
        _productCollectionService = productCollectionService;
    }

    public async Task<IActionResult> Detail(int id)
    {
        IDataResult<ProductCollectionGetDto> collection = await _productCollectionService.GetByIdAsync(id, Includes.ProductCollectionIncludes);
        return View(collection);
    }
}
