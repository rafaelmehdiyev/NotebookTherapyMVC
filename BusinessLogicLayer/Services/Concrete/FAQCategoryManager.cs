namespace BusinessLogicLayer.Services.Concrete;

public class FAQCategoryManager : IFAQCategoryService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public FAQCategoryManager(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

	#region Get Requests
	public async Task<IDataResult<List<FAQCategoryGetDto>>> GetAllAsync(bool getDeleted, params string[] includes)
    {
        List<FAQCategory> fAQCategories = getDeleted
            ? await _unitOfWork.FAQCategoryRepository.GetAllAsync(includes: includes)
            : await _unitOfWork.FAQCategoryRepository.GetAllAsync(b => !b.isDeleted, includes);
        if (fAQCategories is null)
        {
            return new ErrorDataResult<List<FAQCategoryGetDto>>(Messages.NotFound(Messages.FAQCategory));
        }
        return new SuccessDataResult<List<FAQCategoryGetDto>>(_mapper.Map<List<FAQCategoryGetDto>>(fAQCategories));
    }
    public async Task<IDataResult<FAQCategoryGetDto>> GetByIdAsync(int id, params string[] includes)
    {
        FAQCategory fAQCategory = await _unitOfWork.FAQCategoryRepository.GetAsync(b => b.Id == id, includes);
        if (fAQCategory is null)
        {
            return new ErrorDataResult<FAQCategoryGetDto>(Messages.NotFound(Messages.FAQCategory));
        }
        return new SuccessDataResult<FAQCategoryGetDto>(_mapper.Map<FAQCategoryGetDto>(fAQCategory));
    }

	#endregion

	#region Post Requests
    public async Task<IResult> CreateAsync(FAQCategoryPostDto dto)
    {
        FAQCategory fAQCategory = _mapper.Map<FAQCategory>(dto);
        await _unitOfWork.FAQCategoryRepository.CreateAsync(fAQCategory);
        int result = await _unitOfWork.SaveAsync();
        if (result is 0)
        {
            return new ErrorResult(Messages.NotCreated(Messages.FAQCategory));
        }
        return new SuccessResult(Messages.Created(Messages.FAQCategory));
    }

	#endregion

	#region Update Requests
    public async Task<IResult> UpdateAsync(FAQCategoryUpdateDto dto)
    {
        FAQCategory fAQCategory = await _unitOfWork.FAQCategoryRepository.GetAsync(b => b.Id == dto.Id && !b.isDeleted);
        fAQCategory = _mapper.Map<FAQCategory>(dto);
        _unitOfWork.FAQCategoryRepository.Update(fAQCategory);
		int result = await _unitOfWork.SaveAsync();
		if (result is 0)
		{
			return new ErrorResult(Messages.NotUpdated(Messages.FAQCategory));
		}
		return new SuccessResult(Messages.Updated(Messages.FAQCategory));
	}
	public async Task<IResult> RecoverByIdAsync(int id)
	{
		FAQCategory fAQCategory = await _unitOfWork.FAQCategoryRepository.GetAsync(b => b.Id == id && b.isDeleted);
		fAQCategory.isDeleted = false;
		_unitOfWork.FAQCategoryRepository.Update(fAQCategory);
		int result = await _unitOfWork.SaveAsync();
		if (result is 0)
		{
			return new ErrorResult(Messages.NotRecovered(Messages.FAQCategory));
		}
		return new SuccessResult(Messages.Recovered(Messages.FAQCategory));
	}

	#endregion

	#region Delete Requests
	public async Task<IResult> HardDeleteByIdAsync(int id)
    {
        FAQCategory fAQCategory = await _unitOfWork.FAQCategoryRepository.GetAsync(b => b.Id == id && b.isDeleted);
        _unitOfWork.FAQCategoryRepository.Delete(fAQCategory);
        int result = await _unitOfWork.SaveAsync();
        if (result is 0)
        {
            return new ErrorResult(Messages.NotDeleted(Messages.FAQCategory));
        }
        return new SuccessResult(Messages.Deleted(Messages.FAQCategory));
    }
    public async Task<IResult> SoftDeleteByIdAsync(int id)
    {
        FAQCategory fAQCategory = await _unitOfWork.FAQCategoryRepository.GetAsync(b => b.Id == id && !b.isDeleted);
        fAQCategory.isDeleted = true;
        _unitOfWork.FAQCategoryRepository.Update(fAQCategory);
        int result = await _unitOfWork.SaveAsync();
        if (result is 0)
        {
            return new ErrorResult(Messages.NotDeleted(Messages.FAQCategory));
        }
        return new SuccessResult(Messages.Deleted(Messages.FAQCategory));
    }

	#endregion
}


