namespace BusinessLogicLayer.Services.Abstract;
public interface IShippingService
{
    Task<IDataResult<List<ShippingGetDto>>> GetAllAsync(bool getDeleted, params string[] includes);
    Task<IDataResult<ShippingGetDto>> GetByIdAsync(int id, params string[] includes);
    Task<IDataResult<ShippingGetDto>> CreateAsync(ShippingPostDto dto);
    Task<IResult> UpdateAsync(ShippingUpdateDto dto);
    Task<IResult> RecoverByIdAsync(int id);
    Task<IResult> SoftDeleteByIdAsync(int id);
    Task<IResult> HardDeleteByIdAsync(int id);
}
