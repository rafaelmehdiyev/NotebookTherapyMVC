using Entities.Abstract;

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

	#region Get Requests
	public async Task<IDataResult<List<ReviewGetDto>>> GetAllAsync(bool getDeleted, params string[] includes)
    {
        List<Review> reviews = getDeleted
            ? await _unitOfWork.ReviewRepository.GetAllAsync(includes: includes)
            : await _unitOfWork.ReviewRepository.GetAllAsync(b => !b.isDeleted, includes);
        if (reviews is null)
        {
            return new ErrorDataResult<List<ReviewGetDto>>(Messages.NotFound(Messages.Review));
        }
        return new SuccessDataResult<List<ReviewGetDto>>(_mapper.Map<List<ReviewGetDto>>(reviews));
    }
    public async Task<IDataResult<ReviewGetDto>> GetByIdAsync(int id, params string[] includes)
    {
        Review review = await _unitOfWork.ReviewRepository.GetAsync(b => b.Id == id, includes);
        if (review is null)
        {
            return new ErrorDataResult<ReviewGetDto>(Messages.NotFound(Messages.Review));
        }
        return new SuccessDataResult<ReviewGetDto>(_mapper.Map<ReviewGetDto>(review));
    }

	#endregion

	#region Post Requests
    public async Task<IResult> CreateAsync(ReviewPostDto dto)
    {
        Review review = _mapper.Map<Review>(dto);
        await _unitOfWork.ReviewRepository.CreateAsync(review);
        int result = await _unitOfWork.SaveAsync();
        await UpdateProductTotalRatingAsync(dto.ProductId);
        if (result is 0)
        {
            return new ErrorResult(Messages.NotCreated(Messages.Review));
        }
        return new SuccessResult(Messages.Created(Messages.Review));
    }

	#endregion

	#region Update Requests
    public async Task<IResult> UpdateAsync(ReviewUpdateDto dto)
    {
        Review review = await _unitOfWork.ReviewRepository.GetAsync(b => b.Id == dto.Id && !b.isDeleted);
        review = _mapper.Map<Review>(dto);
        _unitOfWork.ReviewRepository.Update(review);
        int result = await _unitOfWork.SaveAsync();
        if (result is 0)
        {
            return new ErrorResult(Messages.NotUpdated(Messages.Review));
        }
        return new SuccessResult(Messages.Updated(Messages.Review));
    }
	public async Task<IResult> RecoverByIdAsync(int id)
	{
		Review review = await _unitOfWork.ReviewRepository.GetAsync(b => b.Id == id && b.isDeleted);
		review.isDeleted = false;
		_unitOfWork.ReviewRepository.Update(review);
		int result = await _unitOfWork.SaveAsync();
		if (result is 0)
		{
			return new ErrorResult(Messages.NotRecovered(Messages.Review));
		}
		return new SuccessResult(Messages.Recovered(Messages.Review));
	}

	#endregion

	#region Delete Requests
	public async Task<IResult> HardDeleteByIdAsync(int id)
    {
        Review review = await _unitOfWork.ReviewRepository.GetAsync(b => b.Id == id && !b.isDeleted);
        _unitOfWork.ReviewRepository.Delete(review);
        int result = await _unitOfWork.SaveAsync();
        if (result is 0)
        {
            return new ErrorResult(Messages.NotDeleted(Messages.Review));
        }
        return new SuccessResult(Messages.Deleted(Messages.Review));
    }
    public async Task<IResult> SoftDeleteByIdAsync(int id)
    {
        Review review = await _unitOfWork.ReviewRepository.GetAsync(b => b.Id == id && !b.isDeleted);
        review.isDeleted = true;
        _unitOfWork.ReviewRepository.Update(review);
        int result = await _unitOfWork.SaveAsync();
        if (result is 0)
        {
            return new ErrorResult(Messages.NotDeleted(Messages.Review));
        }
        return new SuccessResult(Messages.Deleted(Messages.Review));
    }

	#endregion

    private async Task UpdateProductTotalRatingAsync(int id)
    {
        Product product = await _unitOfWork.ProductRepository.GetAsync(p => p.Id == id, "ProductImages","Reviews");
        product.TotalRating = product.Reviews is not null ? (decimal)product.Reviews.Average(r => (int)r.Rating) : 0;
        _unitOfWork.ProductRepository.Update(product);
        await _unitOfWork.SaveAsync();
    }
}
