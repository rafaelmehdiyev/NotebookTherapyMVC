namespace BusinessLogicLayer.Services.Concrete;
public class SizeManager : ISizeService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public SizeManager(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
	#region Get Requests
	public async Task<IDataResult<List<SizeGetDto>>> GetAllAsync(params string[] includes)
    {
        List<Size> sizes = await _unitOfWork.SizeRepository.GetAllAsync(includes: includes);
        if (sizes is null)
        {
            return new ErrorDataResult<List<SizeGetDto>>(Messages.NotFound(Messages.Size));
        }
        return new SuccessDataResult<List<SizeGetDto>>(_mapper.Map<List<SizeGetDto>>(sizes));
    }
    public async Task<IDataResult<SizeGetDto>> GetByIdAsync(int id, params string[] includes)
    {
        Size size = await _unitOfWork.SizeRepository.GetAsync(b => b.Id == id, includes);
        if (size is null)
        {
            return new ErrorDataResult<SizeGetDto>(Messages.NotFound(Messages.Size));
        }
        return new SuccessDataResult<SizeGetDto>(_mapper.Map<SizeGetDto>(size));
    }

	#endregion

	#region Post Requests
    public async Task<IResult> CreateAsync(SizePostDto dto)
    {
        Size size = _mapper.Map<Size>(dto);
        await _unitOfWork.SizeRepository.CreateAsync(size);
        int result = await _unitOfWork.SaveAsync();
        if (result is 0)
        {
            return new ErrorResult(Messages.NotCreated(Messages.Size));
        }
        return new SuccessResult(Messages.Created(Messages.Size));
    }

	#endregion

	#region Update Requests
    public async Task<IResult> UpdateAsync(SizeUpdateDto dto)
    {
        Size size = await _unitOfWork.SizeRepository.GetAsync(b => b.Id == dto.Id && !b.isDeleted);
        size = _mapper.Map<Size>(dto);
        _unitOfWork.SizeRepository.Update(size);
        int result = await _unitOfWork.SaveAsync();
        if (result is 0)
        {
            return new ErrorResult(Messages.NotUpdated(Messages.Size));
        }
        return new SuccessResult(Messages.Updated(Messages.Size));
    }
	public async Task<IResult> RecoverByIdAsync(int id)
	{
		Size size = await _unitOfWork.SizeRepository.GetAsync(b => b.Id == id && b.isDeleted);
		size.isDeleted = false;
		_unitOfWork.SizeRepository.Update(size);
		int result = await _unitOfWork.SaveAsync();
		if (result is 0)
		{
			return new ErrorResult(Messages.NotRecovered(Messages.Size));
		}
		return new SuccessResult(Messages.Recovered(Messages.Size));
	}

	#endregion

	#region Delete Requests
	public async Task<IResult> HardDeleteByIdAsync(int id)
    {
        Size size = await _unitOfWork.SizeRepository.GetAsync(b => b.Id == id && !b.isDeleted);
        _unitOfWork.SizeRepository.Delete(size);
        int result = await _unitOfWork.SaveAsync();
        if (result is 0)
        {
            return new ErrorResult(Messages.NotDeleted(Messages.Size));
        }
        return new SuccessResult(Messages.Deleted(Messages.Size));
    }
    public async Task<IResult> SoftDeleteByIdAsync(int id)
    {
        Size size = await _unitOfWork.SizeRepository.GetAsync(b => b.Id == id && !b.isDeleted);
        size.isDeleted = true;
        _unitOfWork.SizeRepository.Update(size);
        int result = await _unitOfWork.SaveAsync();
        if (result is 0)
        {
            return new ErrorResult(Messages.NotDeleted(Messages.Size));
        }
        return new SuccessResult(Messages.Deleted(Messages.Size));
    }

	#endregion
}