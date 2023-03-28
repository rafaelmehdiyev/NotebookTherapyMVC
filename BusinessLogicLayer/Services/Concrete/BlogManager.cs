namespace BusinessLogicLayer.Services.Concrete;

public class BlogManager : IBlogService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public BlogManager(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

	#region Get Requests
	public async Task<IDataResult<List<BlogGetDto>>> GetAllAsync(params string[] includes)
	{
		List<Blog> blogs = await _unitOfWork.BlogRepository.GetAllAsync(includes:includes);
		if (blogs is null)
		{
			return new ErrorDataResult<List<BlogGetDto>>("Bloglar Tapilmadi");
		}
		return new SuccessDataResult<List<BlogGetDto>>(_mapper.Map<List<BlogGetDto>>(blogs));
	}
	public async Task<IDataResult<BlogGetDto>> GetByIdAsync(int id, params string[] includes)
	{
		Blog blog = await _unitOfWork.BlogRepository.GetAsync(b => b.Id == id, includes);
		if (blog is null)
		{
			return new ErrorDataResult<BlogGetDto>("Blog Tapilmadi");
		}
		return new SuccessDataResult<BlogGetDto>(_mapper.Map<BlogGetDto>(blog));
	}
	#endregion

	#region Post Requests
	public async Task<IResult> CreateAsync(BlogPostDto dto)
	{
		Blog blog = _mapper.Map<Blog>(dto);
		await _unitOfWork.BlogRepository.CreateAsync(blog);
		int result = await _unitOfWork.SaveAsync();
		if (result is 0)
		{
			return new ErrorResult("Blog Yaradila bilmedi");
		}
		return new SuccessResult("Blog Yaradildi");
	}

	#endregion

	#region Update Requests
	public async Task<IResult> IncreaseViewCount(int id)
	{
		Blog blog = await _unitOfWork.BlogRepository.GetAsync(b => b.Id == id && !b.isDeleted);
		blog.ViewCount++;
		_unitOfWork.BlogRepository.Update(blog);
		int result = await _unitOfWork.SaveAsync();
		if (result is 0)
		{
			return new ErrorResult("Blog Yenilene bilmedi");
		}
		return new SuccessResult("Blog Yenilendi");
	}

	public async Task<IResult> UpdateAsync(BlogUpdateDto dto)
	{
		Blog blog = await _unitOfWork.BlogRepository.GetAsync(b => b.Id == dto.Id && !b.isDeleted);
		blog = _mapper.Map<Blog>(dto);
		_unitOfWork.BlogRepository.Update(blog);
		int result = await _unitOfWork.SaveAsync();
		if (result is 0)
		{
			return new ErrorResult("Blog Yenilene bilmedi");
		}
		return new SuccessResult("Blog Yenilendi");
	}

	public async Task<IResult> RecoverByIdAsync(int id)
	{
		Blog blog = await _unitOfWork.BlogRepository.GetAsync(b => b.Id == id && b.isDeleted);
		blog.isDeleted = false;
		_unitOfWork.BlogRepository.Update(blog);
		int result = await _unitOfWork.SaveAsync();
		if (result is 0)
		{
			return new ErrorResult("Blog is not recovered");
		}
		return new SuccessResult("Blog is recovered");
	}
	#endregion

	#region Delete Requests
	public async Task<IResult> HardDeleteByIdAsync(int id)
	{
		Blog blog = await _unitOfWork.BlogRepository.GetAsync(b => b.Id == id && !b.isDeleted);
		_unitOfWork.BlogRepository.Delete(blog);
		int result = await _unitOfWork.SaveAsync();
		if (result is 0)
		{
			return new ErrorResult("Blog Siline bilmedi");
		}
		return new SuccessResult("Blog Silindi");
	}

	public async Task<IResult> SoftDeleteByIdAsync(int id)
	{
		Blog blog = await _unitOfWork.BlogRepository.GetAsync(b => b.Id == id && !b.isDeleted);
		blog.isDeleted = true;
		_unitOfWork.BlogRepository.Update(blog);
		int result = await _unitOfWork.SaveAsync();
		if (result is 0)
		{
			return new ErrorResult("Blog Siline bilmedi");
		}
		return new SuccessResult("Blog Silindi");
	}
	#endregion
}
