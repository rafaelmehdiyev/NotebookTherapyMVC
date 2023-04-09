namespace BusinessLogicLayer.Services.Abstract;
public interface IBlogService : IGenericService<BlogGetDto, BlogPostDto, BlogUpdateDto>
{
    Task<IResult> IncreaseViewCount(int id);
    Task<IDataResult<List<BlogGetDto>>> GetAllPaginateAsync(int page, int size,bool getDeleted, params string[] includes);
}
