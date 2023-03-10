namespace BusinessLogicLayer.Services.Concrete;

public class CategoryManager : ICategoryService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CategoryManager(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IDataResult<List<CategoryGetDto>>> GetAllAsync(params string[] includes)
    {
        List<Category> categories = await _unitOfWork.CategoryRepository.GetAllAsync(p => !p.isDeleted, includes);
        if (categories is null)
        {
            return new ErrorDataResult<List<CategoryGetDto>>("Categorylar Tapilmadi");
        }
        return new SuccessDataResult<List<CategoryGetDto>>(_mapper.Map<List<CategoryGetDto>>(categories));
    }

    public async Task<IDataResult<CategoryGetDto>> GetByIdAsync(int id, params string[] includes)
    {
        Category category = await _unitOfWork.CategoryRepository.GetAsync(b => b.Id == id && !b.isDeleted, includes);
        if (category is null)
        {
            return new ErrorDataResult<CategoryGetDto>("Category Tapilmadi");
        }
        return new SuccessDataResult<CategoryGetDto>(_mapper.Map<CategoryGetDto>(category));
    }

    public async Task<IResult> CreateAsync(CategoryPostDto dto)
    {
        Category category = _mapper.Map<Category>(dto);
        await _unitOfWork.CategoryRepository.CreateAsync(category);
        int result = await _unitOfWork.SaveAsync();
        if (result is 0)
        {
            return new ErrorResult("Category Yaradila bilmedi");
        }
        return new SuccessResult("Category Yaradildi");
    }
    public async Task<IResult> UpdateAsync(CategoryUpdateDto dto)
    {
        Category category = await _unitOfWork.CategoryRepository.GetAsync(b => b.Id == dto.Id && !b.isDeleted);
        category = _mapper.Map<Category>(dto);
        _unitOfWork.CategoryRepository.Update(category);
        int result = await _unitOfWork.SaveAsync();
        if (result is 0)
        {
            return new ErrorResult("Category Yenilene bilmedi");
        }
        return new SuccessResult("Category Yenilendi");
    }
    public async Task<IResult> HardDeleteByIdAsync(int id)
    {
        Category category = await _unitOfWork.CategoryRepository.GetAsync(b => b.Id == id && !b.isDeleted);
        _unitOfWork.CategoryRepository.Delete(category);
        int result = await _unitOfWork.SaveAsync();
        if (result is 0)
        {
            return new ErrorResult("Category Siline bilmedi");
        }
        return new SuccessResult("Category Silindi");
    }

    public async Task<IResult> SoftDeleteByIdAsync(int id)
    {
        Category category = await _unitOfWork.CategoryRepository.GetAsync(b => b.Id == id && !b.isDeleted);
        category.isDeleted = true;
        _unitOfWork.CategoryRepository.Update(category);
        int result = await _unitOfWork.SaveAsync();
        if (result is 0)
        {
            return new ErrorResult("Category Siline bilmedi");
        }
        return new SuccessResult("Category Silindi");
    }
}


