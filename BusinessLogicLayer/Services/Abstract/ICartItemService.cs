namespace BusinessLogicLayer.Services.Abstract;
public interface ICartItemService
{
    Task<IDataResult<List<CartItemGetDto>>> GetAllAsync(params string[] includes);
    Task<IDataResult<CartItemGetDto>> GetByIdAsync(int id, params string[] includes);
    Task<IDataResult<List<CartItemGetDto>>> GetAllByCartIdAsync(int id, params string[] includes);
    Task<IDataResult<CartItemGetDto>> CreateAsync(ProductGetDto dto, UserGetDto user);
    Task<IDataResult<CartItemGetDto>> RemoveItemFromCartAsync(ProductGetDto dto, UserGetDto user,bool deleteAll = false);
    Task<IResult> SoftDeleteByIdAsync(int id);
    Task<IResult> HardDeleteByIdAsync(int id);
}
