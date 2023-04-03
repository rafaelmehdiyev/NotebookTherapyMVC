namespace BusinessLogicLayer.Services.Concrete;

public class FAQManager : IFAQService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public FAQManager(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

	#region Get Requests
	public async Task<IDataResult<List<FAQGetDto>>> GetAllAsync(bool getDeleted, params string[] includes)
    {
        List<FAQ> FAQs = getDeleted
            ? await _unitOfWork.FAQRepository.GetAllAsync(includes: includes)
            : await _unitOfWork.FAQRepository.GetAllAsync(b => !b.isDeleted, includes);
        if (FAQs is null)
        {
            return new ErrorDataResult<List<FAQGetDto>>(Messages.NotFound(Messages.FAQ));
        }
        return new SuccessDataResult<List<FAQGetDto>>(_mapper.Map<List<FAQGetDto>>(FAQs));
    }
    public async Task<IDataResult<List<FAQGetDto>>> GetFaqsByCategoryIdAsync(int id, params string[] includes)
    {
        List<FAQ> faq = await _unitOfWork.FAQRepository.GetAllAsync(b => b.FAQCategoryId == id, includes);
        if (faq is null)
        {
            return new ErrorDataResult<List<FAQGetDto>>(Messages.NotFound(Messages.FAQ));
        }
        return new SuccessDataResult<List<FAQGetDto>>(_mapper.Map<List<FAQGetDto>>(faq));
    }
    public async Task<IDataResult<FAQGetDto>> GetByIdAsync(int id, params string[] includes)
    {
        FAQ faq = await _unitOfWork.FAQRepository.GetAsync(b => b.Id == id, includes);
        if (faq is null)
        {
            return new ErrorDataResult<FAQGetDto>(Messages.NotFound(Messages.FAQ));
        }
        return new SuccessDataResult<FAQGetDto>(_mapper.Map<FAQGetDto>(faq));
    }

	#endregion

	#region Post Requests
    public async Task<IResult> CreateAsync(FAQPostDto dto)
    {
        FAQ faq = _mapper.Map<FAQ>(dto);
		faq.isAnswered = faq.Answer is null ? false : true;
		await _unitOfWork.FAQRepository.CreateAsync(faq);
        int result = await _unitOfWork.SaveAsync();
        if (result is 0)
        {
            return new ErrorResult(Messages.NotCreated(Messages.FAQ));
        }
        return new SuccessResult(Messages.Created(Messages.FAQ));
    }

	#endregion

	#region Update Requests
    public async Task<IResult> UpdateAsync(FAQUpdateDto dto)
    {
        FAQ faq = await _unitOfWork.FAQRepository.GetAsync(b => b.Id == dto.Id && !b.isDeleted);
        faq = _mapper.Map<FAQ>(dto);
        faq.isAnswered = faq.Answer is null ? false : true;
        _unitOfWork.FAQRepository.Update(faq);
        int result = await _unitOfWork.SaveAsync();
        if (result is 0)
        {
            return new ErrorResult(Messages.NotUpdated(Messages.FAQ));
        }
        return new SuccessResult(Messages.Updated(Messages.FAQ));
    }
	public async Task<IResult> RecoverByIdAsync(int id)
	{
		FAQ faq = await _unitOfWork.FAQRepository.GetAsync(b => b.Id == id && b.isDeleted);
		faq.isDeleted = false;
		_unitOfWork.FAQRepository.Update(faq);
		int result = await _unitOfWork.SaveAsync();
		if (result is 0)
		{
			return new ErrorResult(Messages.NotRecovered(Messages.FAQ));
		}
		return new SuccessResult(Messages.Recovered(Messages.FAQ));
	}

	#endregion

	#region Delete Requests
	public async Task<IResult> HardDeleteByIdAsync(int id)
    {
        FAQ faq = await _unitOfWork.FAQRepository.GetAsync(b => b.Id == id && !b.isDeleted);
        _unitOfWork.FAQRepository.Delete(faq);
        int result = await _unitOfWork.SaveAsync();
        if (result is 0)
        {
            return new ErrorResult(Messages.NotDeleted(Messages.FAQ));
        }
        return new SuccessResult(Messages.Deleted(Messages.FAQ));
    }
    public async Task<IResult> SoftDeleteByIdAsync(int id)
    {
        FAQ faq = await _unitOfWork.FAQRepository.GetAsync(b => b.Id == id && !b.isDeleted);
        faq.isDeleted = true;
        _unitOfWork.FAQRepository.Update(faq);
        int result = await _unitOfWork.SaveAsync();
        if (result is 0)
        {
            return new ErrorResult(Messages.NotDeleted(Messages.FAQ));
        }
        return new SuccessResult(Messages.Deleted(Messages.FAQ));
    }

	#endregion
}


