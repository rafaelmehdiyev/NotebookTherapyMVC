namespace BusinessLogicLayer.Services.Concrete;

public class ReviewManager : IReviewService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ReviewManager(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IDataResult<List<ReviewGetDto>>> GetAllAsync(params string[] includes)
    {
        List<Review> reviews = await _unitOfWork.ReviewRepository.GetAllAsync(p => !p.isDeleted, includes);
        if (reviews is null)
        {
            return new ErrorDataResult<List<ReviewGetDto>>("Reviewlar Tapilmadi");
        }
        return new SuccessDataResult<List<ReviewGetDto>>(_mapper.Map<List<ReviewGetDto>>(reviews));
    }

    public async Task<IDataResult<ReviewGetDto>> GetByIdAsync(int id, params string[] includes)
    {
        Review review = await _unitOfWork.ReviewRepository.GetAsync(b => b.Id == id && !b.isDeleted, includes);
        if (review is null)
        {
            return new ErrorDataResult<ReviewGetDto>("Review Tapilmadi");
        }
        return new SuccessDataResult<ReviewGetDto>(_mapper.Map<ReviewGetDto>(review));
    }

    public async Task<IResult> CreateAsync(ReviewPostDto dto)
    {
        Review review = _mapper.Map<Review>(dto);
        await _unitOfWork.ReviewRepository.CreateAsync(review);
        int result = await _unitOfWork.SaveAsync();
        if (result is 0)
        {
            return new ErrorResult("Review Yaradila bilmedi");
        }
        return new SuccessResult("Review Yaradildi");
    }
    public async Task<IResult> UpdateAsync(ReviewUpdateDto dto)
    {
        Review review = await _unitOfWork.ReviewRepository.GetAsync(b => b.Id == dto.Id && !b.isDeleted);
        review = _mapper.Map<Review>(dto);
        _unitOfWork.ReviewRepository.Update(review);
        int result = await _unitOfWork.SaveAsync();
        if (result is 0)
        {
            return new ErrorResult("Review Yenilene bilmedi");
        }
        return new SuccessResult("Review Yenilendi");
    }
    public async Task<IResult> HardDeleteByIdAsync(int id)
    {
        Review review = await _unitOfWork.ReviewRepository.GetAsync(b => b.Id == id && !b.isDeleted);
        _unitOfWork.ReviewRepository.Delete(review);
        int result = await _unitOfWork.SaveAsync();
        if (result is 0)
        {
            return new ErrorResult("Review Siline bilmedi");
        }
        return new SuccessResult("Review Silindi");
    }

    public async Task<IResult> SoftDeleteByIdAsync(int id)
    {
        Review review = await _unitOfWork.ReviewRepository.GetAsync(b => b.Id == id && !b.isDeleted);
        review.isDeleted = true;
        _unitOfWork.ReviewRepository.Update(review);
        int result = await _unitOfWork.SaveAsync();
        if (result is 0)
        {
            return new ErrorResult("Review Siline bilmedi");
        }
        return new SuccessResult("Review Silindi");
    }
}
