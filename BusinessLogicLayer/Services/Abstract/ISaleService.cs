namespace BusinessLogicLayer.Services.Abstract;
public interface ISaleService
{
    Task<IDataResult<List<SaleGetDto>>> GetAllAsync(bool getDeleted, params string[] includes);
    Task<IDataResult<SaleGetDto>> GetByIdAsync(int id, params string[] includes);
    Task<IDataResult<SaleGetDto>> CreateAsync(SalePostDto dto);
    Task<IResult> UpdateAsync(SaleUpdateDto dto);
    Task<IResult> RecoverByIdAsync(int id);
    Task<IResult> SoftDeleteByIdAsync(int id);
    Task<IResult> HardDeleteByIdAsync(int id);
}
