namespace BusinessLogicLayer.Services.Concrete;

public class BlogManager : IBlogService
{
    public Task<IDataResult<List<BlogGetDto>>> GetAllAsync(params string[] includes)
    {
        throw new NotImplementedException();
    }

    public Task<IDataResult<BlogGetDto>> GetByIdAsync(int id, params string[] includes)
    {
        throw new NotImplementedException();
    }

    public Task<IDataResult<BlogGetDto>> GetByNameAsync(string name, params string[] includes)
    {
        throw new NotImplementedException();
    }
    public Task<IResult> CreateAsync(BlogPostDto dto)
    {
        throw new NotImplementedException();
    }
    public Task<IResult> UpdateAsync(BlogUpdateDto dto)
    {
        throw new NotImplementedException();
    }
    public Task<IResult> HardDeleteByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IResult> SoftDeleteByIdAsync(int id)
    {
        throw new NotImplementedException();
    }
}
