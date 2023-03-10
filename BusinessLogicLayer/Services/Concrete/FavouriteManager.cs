namespace BusinessLogicLayer.Services.Concrete;

public class FavouriteManager : IFavouriteService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public FavouriteManager(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IDataResult<List<FavouriteGetDto>>> GetAllAsync(params string[] includes)
    {
        List<Favourite> blogs = await _unitOfWork.FavouriteRepository.GetAllAsync(p => !p.isDeleted, includes);
        if (blogs is null)
        {
            return new ErrorDataResult<List<FavouriteGetDto>>("Favouritelar Tapilmadi");
        }
        return new SuccessDataResult<List<FavouriteGetDto>>(_mapper.Map<List<FavouriteGetDto>>(blogs));
    }

    public async Task<IDataResult<FavouriteGetDto>> GetByIdAsync(int id, params string[] includes)
    {
        Favourite blog = await _unitOfWork.FavouriteRepository.GetAsync(b => b.Id == id && !b.isDeleted, includes);
        if (blog is null)
        {
            return new ErrorDataResult<FavouriteGetDto>("Favourite Tapilmadi");
        }
        return new SuccessDataResult<FavouriteGetDto>(_mapper.Map<FavouriteGetDto>(blog));
    }

    public async Task<IResult> CreateAsync(FavouritePostDto dto)
    {
        Favourite blog = _mapper.Map<Favourite>(dto);
        await _unitOfWork.FavouriteRepository.CreateAsync(blog);
        int result = await _unitOfWork.SaveAsync();
        if (result is 0)
        {
            return new ErrorResult("Favourite Yaradila bilmedi");
        }
        return new SuccessResult("Favourite Yaradildi");
    }
    public async Task<IResult> UpdateAsync(FavouriteUpdateDto dto)
    {
        Favourite blog = await _unitOfWork.FavouriteRepository.GetAsync(b => b.Id == dto.Id && !b.isDeleted);
        blog = _mapper.Map<Favourite>(dto);
        _unitOfWork.FavouriteRepository.Update(blog);
        int result = await _unitOfWork.SaveAsync();
        if (result is 0)
        {
            return new ErrorResult("Favourite Yenilene bilmedi");
        }
        return new SuccessResult("Favourite Yenilendi");
    }
    public async Task<IResult> HardDeleteByIdAsync(int id)
    {
        Favourite blog = await _unitOfWork.FavouriteRepository.GetAsync(b => b.Id == id && !b.isDeleted);
        _unitOfWork.FavouriteRepository.Delete(blog);
        int result = await _unitOfWork.SaveAsync();
        if (result is 0)
        {
            return new ErrorResult("Favourite Siline bilmedi");
        }
        return new SuccessResult("Favourite Silindi");
    }

    public async Task<IResult> SoftDeleteByIdAsync(int id)
    {
        Favourite blog = await _unitOfWork.FavouriteRepository.GetAsync(b => b.Id == id && !b.isDeleted);
        blog.isDeleted = true;
        _unitOfWork.FavouriteRepository.Update(blog);
        int result = await _unitOfWork.SaveAsync();
        if (result is 0)
        {
            return new ErrorResult("Favourite Siline bilmedi");
        }
        return new SuccessResult("Favourite Silindi");
    }
}


