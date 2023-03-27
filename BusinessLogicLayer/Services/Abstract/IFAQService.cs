namespace BusinessLogicLayer.Services.Abstract;
public interface IFAQService : IGenericService<FAQGetDto, FAQPostDto, FAQUpdateDto>
{
    Task<IDataResult<List<FAQGetDto>>> GetFaqsByCategoryIdAsync(int id, params string[] includes);
}
