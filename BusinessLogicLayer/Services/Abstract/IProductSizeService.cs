namespace BusinessLogicLayer.Services.Abstract;

public interface IProductSizeService
{
	Task<IDataResult<List<ProductSizeGetDto>>> GetAllAsync(params string[] includes);
	Task<IDataResult<ProductSizeGetDto>> GetByIdAsync(int id, params string[] includes);
	Task<IResult> CreateAsync(ProductSizePostDto dto);
	Task<IResult> UpdateAsync(ProductSizeUpdateDto dto);
	Task<IResult> HardDeleteByIdAsync(int id);
}
