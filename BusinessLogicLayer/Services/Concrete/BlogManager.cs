namespace BusinessLogicLayer.Services.Concrete;

public class BlogManager : IBlogService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IWebHostEnvironment _env;
    public BlogManager(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment env)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _env = env;
    }

    #region Get Requests
    public async Task<IDataResult<List<BlogGetDto>>> GetAllAsync(bool getDeleted, params string[] includes)
    {
        List<Blog> blogs = getDeleted
            ? await _unitOfWork.BlogRepository.GetAllAsync(includes: includes)
            : await _unitOfWork.BlogRepository.GetAllAsync(b => !b.isDeleted, includes);
        if (blogs is null)
        {
            return new ErrorDataResult<List<BlogGetDto>>(Messages.NotFound(Messages.Blog));
        }
        return new SuccessDataResult<List<BlogGetDto>>(_mapper.Map<List<BlogGetDto>>(blogs));
    }
    public async Task<IDataResult<List<BlogGetDto>>> GetAllPaginateAsync(int page,int size,bool getDeleted, params string[] includes)
    {
        List<Blog> blogs = getDeleted
            ? await _unitOfWork.BlogRepository.GetAllPaginateAsync(page,size,includes: includes)
            : await _unitOfWork.BlogRepository.GetAllPaginateAsync(page,size,b => !b.isDeleted, includes);
        if (blogs is null)
        {
            return new ErrorDataResult<List<BlogGetDto>>(Messages.NotFound(Messages.Blog));
        }
        return new SuccessDataResult<List<BlogGetDto>>(_mapper.Map<List<BlogGetDto>>(blogs));
    }
    public async Task<IDataResult<BlogGetDto>> GetByIdAsync(int id, params string[] includes)
    {
        Blog blog = await _unitOfWork.BlogRepository.GetAsync(b => b.Id == id, includes);
        if (blog is null)
        {
            return new ErrorDataResult<BlogGetDto>(Messages.NotFound(Messages.Blog));
        }
        return new SuccessDataResult<BlogGetDto>(_mapper.Map<BlogGetDto>(blog));
    }
    #endregion

    #region Post Requests
    public async Task<IResult> CreateAsync(BlogPostDto dto)
    {
        Blog blog = _mapper.Map<Blog>(dto);
        blog.ImagePath = dto.Image.FileCreate(_env.WebRootPath, "uploads/blog");
        await _unitOfWork.BlogRepository.CreateAsync(blog);
        int result = await _unitOfWork.SaveAsync();
        if (result is 0)
        {
            return new ErrorResult(Messages.NotCreated(Messages.Blog));
        }
        return new SuccessResult(Messages.Created(Messages.Blog));
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
            return new ErrorResult(Messages.NotUpdated(Messages.Blog));
        }
        return new SuccessResult(Messages.Updated(Messages.Blog));
    }

    public async Task<IResult> UpdateAsync(BlogUpdateDto dto)
    {
        Blog blog = await _unitOfWork.BlogRepository.GetAsync(b => b.Id == dto.Id && !b.isDeleted);
        File.Delete(Path.Combine(_env.WebRootPath, "uploads/blog", blog.ImagePath));
        blog = _mapper.Map<Blog>(dto);
        blog.ImagePath = dto.Image.FileCreate(_env.WebRootPath, "uploads/blog");
        _unitOfWork.BlogRepository.Update(blog);
        int result = await _unitOfWork.SaveAsync();
        if (result is 0)
        {
            return new ErrorResult(Messages.NotUpdated(Messages.Blog));
        }
        return new SuccessResult(Messages.Updated(Messages.Blog));
    }

    public async Task<IResult> RecoverByIdAsync(int id)
    {
        Blog blog = await _unitOfWork.BlogRepository.GetAsync(b => b.Id == id && b.isDeleted);
        blog.isDeleted = false;
        _unitOfWork.BlogRepository.Update(blog);
        int result = await _unitOfWork.SaveAsync();
        if (result is 0)
        {
            return new ErrorResult(Messages.NotRecovered(Messages.Blog));
        }
        return new SuccessResult(Messages.Recovered(Messages.Blog));
    }
    #endregion

    #region Delete Requests
    public async Task<IResult> HardDeleteByIdAsync(int id)
    {
        Blog blog = await _unitOfWork.BlogRepository.GetAsync(b => b.Id == id && b.isDeleted);
        _unitOfWork.BlogRepository.Delete(blog);
        int result = await _unitOfWork.SaveAsync();
        if (result is 0)
        {
            return new ErrorResult(Messages.NotDeleted(Messages.Blog));
        }
        return new SuccessResult(Messages.Deleted(Messages.Blog));
    }

    public async Task<IResult> SoftDeleteByIdAsync(int id)
    {
        Blog blog = await _unitOfWork.BlogRepository.GetAsync(b => b.Id == id && !b.isDeleted);
        blog.isDeleted = true;
        _unitOfWork.BlogRepository.Update(blog);
        int result = await _unitOfWork.SaveAsync();
        if (result is 0)
        {
            return new ErrorResult(Messages.NotDeleted(Messages.Blog));
        }
        return new SuccessResult(Messages.Deleted(Messages.Blog));
    }
    #endregion
}
