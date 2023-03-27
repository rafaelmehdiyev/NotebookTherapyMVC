namespace BusinessLogicLayer.Services.Abstract;
public interface ICartService : IGenericService<CartGetDto, CartPostDto, CartUpdateDto>
{
    Task<IDataResult<CartGetDto>> GetCartByUserIdAsync(string id, params string[] includes);
}