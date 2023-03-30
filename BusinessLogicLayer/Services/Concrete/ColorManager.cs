namespace BusinessLogicLayer.Services.Concrete;

public class ColorManager : IColorService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ColorManager(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

	#region Get Requests
	public async Task<IDataResult<List<ColorGetDto>>> GetAllAsync(params string[] includes)
    {
        List<Color> sizes = await _unitOfWork.ColorRepository.GetAllAsync(includes: includes);
        if (sizes is null)
        {
            return new ErrorDataResult<List<ColorGetDto>>(Messages.NotFound(Messages.Color));
        }
        return new SuccessDataResult<List<ColorGetDto>>(_mapper.Map<List<ColorGetDto>>(sizes));
    }
    public async Task<IDataResult<ColorGetDto>> GetByIdAsync(int id, params string[] includes)
    {
        Color size = await _unitOfWork.ColorRepository.GetAsync(b => b.Id == id, includes);
        if (size is null)
        {
            return new ErrorDataResult<ColorGetDto>(Messages.NotFound(Messages.Color));
        }
        return new SuccessDataResult<ColorGetDto>(_mapper.Map<ColorGetDto>(size));
    }

	#endregion

	#region Post Requests
    public async Task<IResult> CreateAsync(ColorPostDto dto)
    {
        Color size = _mapper.Map<Color>(dto);
        await _unitOfWork.ColorRepository.CreateAsync(size);
        int result = await _unitOfWork.SaveAsync();
        if (result is 0)
        {
            return new ErrorResult(Messages.NotCreated(Messages.Color));
        }
        return new SuccessResult(Messages.Created(Messages.Color));
    }

	#endregion

	#region Update Requests
    public async Task<IResult> UpdateAsync(ColorUpdateDto dto)
    {
        Color size = await _unitOfWork.ColorRepository.GetAsync(b => b.Id == dto.Id && !b.isDeleted);
        size = _mapper.Map<Color>(dto);
        _unitOfWork.ColorRepository.Update(size);
        int result = await _unitOfWork.SaveAsync();
        if (result is 0)
        {
            return new ErrorResult(Messages.NotUpdated(Messages.Color));
        }
        return new SuccessResult(Messages.Updated(Messages.Color));
    }
	public async Task<IResult> RecoverByIdAsync(int id)
	{
		Color size = await _unitOfWork.ColorRepository.GetAsync(b => b.Id == id && b.isDeleted);
		size.isDeleted = false;
		_unitOfWork.ColorRepository.Update(size);
		int result = await _unitOfWork.SaveAsync();
		if (result is 0)
		{
			return new ErrorResult(Messages.NotRecovered(Messages.Color));
		}
		return new SuccessResult(Messages.Recovered(Messages.Color));
	}
	#endregion

	#region Delete Requests
	public async Task<IResult> HardDeleteByIdAsync(int id)
    {
        Color size = await _unitOfWork.ColorRepository.GetAsync(b => b.Id == id && !b.isDeleted);
        _unitOfWork.ColorRepository.Delete(size);
        int result = await _unitOfWork.SaveAsync();
        if (result is 0)
        {
            return new ErrorResult(Messages.NotDeleted(Messages.Color));
        }
        return new SuccessResult(Messages.Deleted(Messages.Color));
    }
    public async Task<IResult> SoftDeleteByIdAsync(int id)
    {
        Color size = await _unitOfWork.ColorRepository.GetAsync(b => b.Id == id && !b.isDeleted);
        size.isDeleted = true;
        _unitOfWork.ColorRepository.Update(size);
        int result = await _unitOfWork.SaveAsync();
        if (result is 0)
        {
            return new ErrorResult(Messages.NotDeleted(Messages.Color));
        }
        return new SuccessResult(Messages.Deleted(Messages.Color));
    }

	#endregion

}