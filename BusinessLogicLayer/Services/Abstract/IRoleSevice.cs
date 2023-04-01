namespace BusinessLogicLayer.Services.Abstract;

public interface IRoleSevice
{
    Task<IDataResult<List<RoleGetDto>>> GetAllAsync();
    Task<IDataResult<RoleGetDto>> GetByIdAsync(string iD);
    Task<IResult> CreateAsync(RolePostDto dto);
    Task<IResult> UpdateAsync(RoleUpdateDto dto);
    Task<IResult> HardDeleteByIdAsync(string id);

}
