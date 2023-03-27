namespace BusinessLogicLayer.Services.Abstract;
public interface IBlogService : IGenericService<BlogGetDto, BlogPostDto, BlogUpdateDto>
{
    Task<IResult> IncreaseViewCount(int id);
}
