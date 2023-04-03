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

	#region Get Requests
	public async Task<IDataResult<List<CategoryGetDto>>> GetAllAsync(bool getDeleted, params string[] includes)
	{
		List<Category> categories = getDeleted
			? await _unitOfWork.CategoryRepository.GetAllAsync(includes: includes)
			: await _unitOfWork.CategoryRepository.GetAllAsync(b => !b.isDeleted, includes);
		if (categories is null)
		{
			return new ErrorDataResult<List<CategoryGetDto>>(Messages.NotFound(Messages.Category));
		}
		return new SuccessDataResult<List<CategoryGetDto>>(_mapper.Map<List<CategoryGetDto>>(categories));
	}

    public async Task<IDataResult<CategoryGetDto>> GetByIdAsync(int id, params string[] includes)
	{
		Category category = await _unitOfWork.CategoryRepository.GetAsync(b => b.Id == id, includes);
		if (category is null)
		{
			return new ErrorDataResult<CategoryGetDto>(Messages.NotFound(Messages.Category));
		}
		return new SuccessDataResult<CategoryGetDto>(_mapper.Map<CategoryGetDto>(category));
	}
	#endregion

	#region Post Requests
	public async Task<IResult> CreateAsync(CategoryPostDto dto)
	{
		Category category = _mapper.Map<Category>(dto);
		await _unitOfWork.CategoryRepository.CreateAsync(category);
		int result = await _unitOfWork.SaveAsync();
		if (result is 0)
		{
			return new ErrorResult(Messages.NotCreated(Messages.Category));
		}
		return new SuccessResult(Messages.Created(Messages.Category));
	}

	#endregion

	#region Update Requests
	public async Task<IResult> UpdateAsync(CategoryUpdateDto dto)
	{
		Category category = await _unitOfWork.CategoryRepository.GetAsync(b => b.Id == dto.Id && !b.isDeleted);
		category = _mapper.Map<Category>(dto);
		_unitOfWork.CategoryRepository.Update(category);
		int result = await _unitOfWork.SaveAsync();
		if (result is 0)
		{
			return new ErrorResult(Messages.NotUpdated(Messages.Category));
		}
		return new SuccessResult(Messages.Updated(Messages.Category));
	}
	public async Task<IResult> RecoverByIdAsync(int id)
	{
		Category category = await _unitOfWork.CategoryRepository.GetAsync(b => b.Id == id && b.isDeleted);
		category.isDeleted = false;
		_unitOfWork.CategoryRepository.Update(category);
		int result = await _unitOfWork.SaveAsync();
		if (result is 0)
		{
			return new ErrorResult(Messages.NotRecovered(Messages.Category));
		}
		return new SuccessResult(Messages.Recovered(Messages.Category));
	}
	#endregion

	#region Delete Requests
	public async Task<IResult> HardDeleteByIdAsync(int id)
    {
        Category category = await _unitOfWork.CategoryRepository.GetAsync(b => b.Id == id && b.isDeleted);
        _unitOfWork.CategoryRepository.Delete(category);
        int result = await _unitOfWork.SaveAsync();
        if (result is 0)
        {
            return new ErrorResult(Messages.NotDeleted(Messages.Category));
        }
        return new SuccessResult(Messages.Deleted(Messages.Category));
    }
    public async Task<IResult> SoftDeleteByIdAsync(int id)
    {
        Category category = await _unitOfWork.CategoryRepository.GetAsync(b => b.Id == id && !b.isDeleted);
        category.isDeleted = true;
        _unitOfWork.CategoryRepository.Update(category);
        int result = await _unitOfWork.SaveAsync();
        if (result is 0)
        {
            return new ErrorResult(Messages.NotDeleted(Messages.Category));
        }
        return new SuccessResult(Messages.Deleted(Messages.Category));
    }
	#endregion
}


