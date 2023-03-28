namespace BusinessLogicLayer.Services.Abstract;

public interface IProductBundleService
{
	Task<IDataResult<List<ProductBundleGetDto>>> GetAllAsync(params string[] includes);
	Task<IDataResult<ProductBundleGetDto>> GetByIdAsync(int id, params string[] includes);
	Task<IResult> CreateAsync(ProductBundlePostDto dto);
	Task<IResult> UpdateAsync(ProductBundleUpdateDto dto);
	Task<IResult> HardDeleteByIdAsync(int id);
}
