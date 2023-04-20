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

    #region Get Requests
    public async Task<IDataResult<List<FavouriteGetDto>>> GetAllAsync(params string[] includes)
    {
        List<Favourite> favourites = await _unitOfWork.FavouriteRepository.GetAllAsync(includes: includes);
        if (favourites is null)
        {
            return new ErrorDataResult<List<FavouriteGetDto>>(Messages.NotFound(Messages.Favourite));
        }
        return new SuccessDataResult<List<FavouriteGetDto>>(_mapper.Map<List<FavouriteGetDto>>(favourites));
    }

    public async Task<IDataResult<List<FavouriteGetDto>>> GetAllByUserIdAsync(string id,params string[] includes)
    {
        List<Favourite> favourites = await _unitOfWork.FavouriteRepository.GetAllAsync(f=>f.UserId == id,includes: includes);
        if (favourites is null)
        {
            return new ErrorDataResult<List<FavouriteGetDto>>(Messages.NotFound(Messages.Favourite));
        }
        return new SuccessDataResult<List<FavouriteGetDto>>(_mapper.Map<List<FavouriteGetDto>>(favourites));
    }

    public async Task<IDataResult<FavouriteGetDto>> GetByIdAsync(int id, params string[] includes)
    {
        Favourite favourite = await _unitOfWork.FavouriteRepository.GetAsync(b => b.Id == id, includes);
        if (favourite is null)
        {
            return new ErrorDataResult<FavouriteGetDto>(Messages.NotFound(Messages.Favourite));
        }
        return new SuccessDataResult<FavouriteGetDto>(_mapper.Map<FavouriteGetDto>(favourite));
    }
    #endregion

    #region Post Requests
    public async Task<IDataResult<FavouriteGetDto>> CreateAsync(FavouritePostDto dto)
    {
        Favourite favourite = _mapper.Map<Favourite>(dto);
        await _unitOfWork.FavouriteRepository.CreateAsync(favourite);
        int result = await _unitOfWork.SaveAsync();
        if (result is 0)
        {
            return new ErrorDataResult<FavouriteGetDto>(Messages.NotCreated(Messages.Favourite));
        }
        return new SuccessDataResult<FavouriteGetDto>(_mapper.Map<FavouriteGetDto>(favourite),"Product Added to Favourites");
    }
    #endregion

    #region Update Requests
    public async Task<IResult> UpdateAsync(FavouriteUpdateDto dto)
    {
        Favourite blog = await _unitOfWork.FavouriteRepository.GetAsync(b => b.Id == dto.Id);
        blog = _mapper.Map<Favourite>(dto);
        _unitOfWork.FavouriteRepository.Update(blog);
        int result = await _unitOfWork.SaveAsync();
        if (result is 0)
        {
            return new ErrorResult(Messages.NotUpdated(Messages.Favourite));
        }
        return new SuccessResult(Messages.Updated(Messages.Favourite));
    }
    #endregion

    #region Delete Requests
    public async Task<IResult> HardDeleteByIdAsync(int id)
    {
        Favourite favourite = await _unitOfWork.FavouriteRepository.GetAsync(b => b.Id == id);
        _unitOfWork.FavouriteRepository.Delete(favourite);
        int result = await _unitOfWork.SaveAsync();
        if (result is 0)
        {
            return new ErrorResult(Messages.NotDeleted(Messages.Favourite));
        }
        return new SuccessResult(Messages.Deleted(Messages.Favourite));
    }
    #endregion
}