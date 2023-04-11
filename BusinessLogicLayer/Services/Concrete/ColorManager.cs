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
	public async Task<IDataResult<List<ColorGetDto>>> GetAllAsync(bool getDeleted, params string[] includes)
    {
        List<Color> colors = getDeleted
            ? await _unitOfWork.ColorRepository.GetAllAsync(includes: includes)
            : await _unitOfWork.ColorRepository.GetAllAsync(b => !b.isDeleted, includes);
        if (colors is null)
        {
            return new ErrorDataResult<List<ColorGetDto>>(Messages.NotFound(Messages.Color));
        }
        return new SuccessDataResult<List<ColorGetDto>>(_mapper.Map<List<ColorGetDto>>(colors));
    }
    public async Task<IDataResult<ColorGetDto>> GetByIdAsync(int id, params string[] includes)
    {
        Color color = await _unitOfWork.ColorRepository.GetAsync(b => b.Id == id, includes);
        if (color is null)
        {
            return new ErrorDataResult<ColorGetDto>(Messages.NotFound(Messages.Color));
        }
        return new SuccessDataResult<ColorGetDto>(_mapper.Map<ColorGetDto>(color));
    }

	#endregion

	#region Post Requests
    public async Task<IResult> CreateAsync(ColorPostDto dto)
    {
        Color color = _mapper.Map<Color>(dto);
        await _unitOfWork.ColorRepository.CreateAsync(color);
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
        Color color = await _unitOfWork.ColorRepository.GetAsync(b => b.Id == dto.Id && !b.isDeleted);
        color = _mapper.Map<Color>(dto);
        _unitOfWork.ColorRepository.Update(color);
        int result = await _unitOfWork.SaveAsync();
        if (result is 0)
        {
            return new ErrorResult(Messages.NotUpdated(Messages.Color));
        }
        return new SuccessResult(Messages.Updated(Messages.Color));
    }
	public async Task<IResult> RecoverByIdAsync(int id)
	{
		Color color = await _unitOfWork.ColorRepository.GetAsync(b => b.Id == id && b.isDeleted);
		color.isDeleted = false;
		_unitOfWork.ColorRepository.Update(color);
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
        Color color = await _unitOfWork.ColorRepository.GetAsync(b => b.Id == id && b.isDeleted);
        _unitOfWork.ColorRepository.Delete(color);
        int result = await _unitOfWork.SaveAsync();
        if (result is 0)
        {
            return new ErrorResult(Messages.NotDeleted(Messages.Color));
        }
        return new SuccessResult(Messages.Deleted(Messages.Color));
    }
    public async Task<IResult> SoftDeleteByIdAsync(int id)
    {
        Color color = await _unitOfWork.ColorRepository.GetAsync(b => b.Id == id && !b.isDeleted);
        color.isDeleted = true;
        _unitOfWork.ColorRepository.Update(color);
        int result = await _unitOfWork.SaveAsync();
        if (result is 0)
        {
            return new ErrorResult(Messages.NotDeleted(Messages.Color));
        }
        return new SuccessResult(Messages.Deleted(Messages.Color));
    }

	#endregion

}