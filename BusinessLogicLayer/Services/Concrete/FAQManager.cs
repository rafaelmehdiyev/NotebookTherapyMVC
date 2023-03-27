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

    public async Task<IDataResult<List<FAQGetDto>>> GetAllAsync(params string[] includes)
    {
        List<FAQ> FAQs = await _unitOfWork.FAQRepository.GetAllAsync(p => !p.isDeleted, includes);
        if (FAQs is null)
        {
            return new ErrorDataResult<List<FAQGetDto>>("FAQlar Tapilmadi");
        }
        return new SuccessDataResult<List<FAQGetDto>>(_mapper.Map<List<FAQGetDto>>(FAQs));
    }

    public async Task<IDataResult<List<FAQGetDto>>> GetFaqsByCategoryIdAsync(int id, params string[] includes)
    {
        List<FAQ> faq = await _unitOfWork.FAQRepository.GetAllAsync(b => b.FAQCategoryId == id && !b.isDeleted, includes);
        if (faq is null)
        {
            return new ErrorDataResult<List<FAQGetDto>>("FAQlar Tapilmadi");
        }
        return new SuccessDataResult<List<FAQGetDto>>(_mapper.Map<List<FAQGetDto>>(faq));
    }

    public async Task<IDataResult<FAQGetDto>> GetByIdAsync(int id, params string[] includes)
    {
        FAQ faq = await _unitOfWork.FAQRepository.GetAsync(b => b.Id == id && !b.isDeleted, includes);
        if (faq is null)
        {
            return new ErrorDataResult<FAQGetDto>("FAQ Tapilmadi");
        }
        return new SuccessDataResult<FAQGetDto>(_mapper.Map<FAQGetDto>(faq));
    }

    public async Task<IResult> CreateAsync(FAQPostDto dto)
    {
        FAQ faq = _mapper.Map<FAQ>(dto);
        await _unitOfWork.FAQRepository.CreateAsync(faq);
        int result = await _unitOfWork.SaveAsync();
        if (result is 0)
        {
            return new ErrorResult("FAQ Yaradila bilmedi");
        }
        return new SuccessResult("FAQ Yaradildi");
    }
    public async Task<IResult> UpdateAsync(FAQUpdateDto dto)
    {
        FAQ faq = await _unitOfWork.FAQRepository.GetAsync(b => b.Id == dto.Id && !b.isDeleted);
        faq = _mapper.Map<FAQ>(dto);
        faq.isAnswered = faq.Answer is null ? false : true;
        _unitOfWork.FAQRepository.Update(faq);
        int result = await _unitOfWork.SaveAsync();
        if (result is 0)
        {
            return new ErrorResult("FAQ Yenilene bilmedi");
        }
        return new SuccessResult("FAQ Yenilendi");
    }
    public async Task<IResult> HardDeleteByIdAsync(int id)
    {
        FAQ faq = await _unitOfWork.FAQRepository.GetAsync(b => b.Id == id && !b.isDeleted);
        _unitOfWork.FAQRepository.Delete(faq);
        int result = await _unitOfWork.SaveAsync();
        if (result is 0)
        {
            return new ErrorResult("FAQ Siline bilmedi");
        }
        return new SuccessResult("FAQ Silindi");
    }

    public async Task<IResult> SoftDeleteByIdAsync(int id)
    {
        FAQ faq = await _unitOfWork.FAQRepository.GetAsync(b => b.Id == id && !b.isDeleted);
        faq.isDeleted = true;
        _unitOfWork.FAQRepository.Update(faq);
        int result = await _unitOfWork.SaveAsync();
        if (result is 0)
        {
            return new ErrorResult("FAQ Siline bilmedi");
        }
        return new SuccessResult("FAQ Silindi");
    }
}


