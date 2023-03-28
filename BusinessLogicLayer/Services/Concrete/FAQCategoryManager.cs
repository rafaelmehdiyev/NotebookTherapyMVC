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
	public async Task<IDataResult<List<FAQCategoryGetDto>>> GetAllAsync(params string[] includes)
    {
        List<FAQCategory> fAQCategories = await _unitOfWork.FAQCategoryRepository.GetAllAsync(includes: includes);
        if (fAQCategories is null)
        {
            return new ErrorDataResult<List<FAQCategoryGetDto>>("FAQCategorylar Tapilmadi");
        }
        return new SuccessDataResult<List<FAQCategoryGetDto>>(_mapper.Map<List<FAQCategoryGetDto>>(fAQCategories));
    }
    public async Task<IDataResult<FAQCategoryGetDto>> GetByIdAsync(int id, params string[] includes)
    {
        FAQCategory fAQCategory = await _unitOfWork.FAQCategoryRepository.GetAsync(b => b.Id == id, includes);
        if (fAQCategory is null)
        {
            return new ErrorDataResult<FAQCategoryGetDto>("FAQCategory Tapilmadi");
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
            return new ErrorResult("FAQCategory Yaradila bilmedi");
        }
        return new SuccessResult("FAQCategory Yaradildi");
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
            return new ErrorResult("FAQCategory Yenilene bilmedi");
        }
        return new SuccessResult("FAQCategory Yenilendi");
    }
	public async Task<IResult> RecoverByIdAsync(int id)
	{
		FAQCategory fAQCategory = await _unitOfWork.FAQCategoryRepository.GetAsync(b => b.Id == id && b.isDeleted);
		fAQCategory.isDeleted = false;
		_unitOfWork.FAQCategoryRepository.Update(fAQCategory);
		int result = await _unitOfWork.SaveAsync();
		if (result is 0)
		{
			return new ErrorResult("FAQCategory is not recovered");
		}
		return new SuccessResult("FAQCategory is recovered");
	}

	#endregion

	#region Delete Requests
	public async Task<IResult> HardDeleteByIdAsync(int id)
    {
        FAQCategory fAQCategory = await _unitOfWork.FAQCategoryRepository.GetAsync(b => b.Id == id && !b.isDeleted);
        _unitOfWork.FAQCategoryRepository.Delete(fAQCategory);
        int result = await _unitOfWork.SaveAsync();
        if (result is 0)
        {
            return new ErrorResult("FAQCategory Siline bilmedi");
        }
        return new SuccessResult("FAQCategory Silindi");
    }
    public async Task<IResult> SoftDeleteByIdAsync(int id)
    {
        FAQCategory fAQCategory = await _unitOfWork.FAQCategoryRepository.GetAsync(b => b.Id == id && !b.isDeleted);
        fAQCategory.isDeleted = true;
        _unitOfWork.FAQCategoryRepository.Update(fAQCategory);
        int result = await _unitOfWork.SaveAsync();
        if (result is 0)
        {
            return new ErrorResult("FAQCategory Siline bilmedi");
        }
        return new SuccessResult("FAQCategory Silindi");
    }

	#endregion
}


