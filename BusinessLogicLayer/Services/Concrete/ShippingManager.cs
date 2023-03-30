namespace BusinessLogicLayer.Services.Concrete;

public class ShippingManager : IShippingService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ShippingManager(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
	#region Get Requests
	public async Task<IDataResult<List<ShippingGetDto>>> GetAllAsync(params string[] includes)
    {
        List<Shipping> shippings = await _unitOfWork.ShippingRepository.GetAllAsync(includes: includes);
        if (shippings is null)
        {
            return new ErrorDataResult<List<ShippingGetDto>>(Messages.NotFound(Messages.Shipping));
        }
        return new SuccessDataResult<List<ShippingGetDto>>(_mapper.Map<List<ShippingGetDto>>(shippings));
    }
    public async Task<IDataResult<ShippingGetDto>> GetByIdAsync(int id, params string[] includes)
    {
        Shipping shipping = await _unitOfWork.ShippingRepository.GetAsync(b => b.Id == id, includes);
        if (shipping is null)
        {
            return new ErrorDataResult<ShippingGetDto>(Messages.NotFound(Messages.Shipping));
        }
        return new SuccessDataResult<ShippingGetDto>(_mapper.Map<ShippingGetDto>(shipping));
    }

	#endregion

	#region Post Requests
    public async Task<IResult> CreateAsync(ShippingPostDto dto)
    {
        Shipping shipping = _mapper.Map<Shipping>(dto);
        await _unitOfWork.ShippingRepository.CreateAsync(shipping);
        int result = await _unitOfWork.SaveAsync();
        if (result is 0)
        {
            return new ErrorResult(Messages.NotCreated(Messages.Shipping));
        }
        return new SuccessResult(Messages.Created(Messages.Shipping));
    }

	#endregion

	#region Update Requests
    public async Task<IResult> UpdateAsync(ShippingUpdateDto dto)
    {
        Shipping shipping = await _unitOfWork.ShippingRepository.GetAsync(b => b.Id == dto.Id && !b.isDeleted, "User");
        shipping = _mapper.Map<Shipping>(dto);
        _unitOfWork.ShippingRepository.Update(shipping);
        int result = await _unitOfWork.SaveAsync();
        if (result is 0)
        {
            return new ErrorResult(Messages.NotUpdated(Messages.Shipping));
        }
        return new SuccessResult(Messages.Updated(Messages.Shipping));
    }
	public async Task<IResult> RecoverByIdAsync(int id)
	{
		Shipping shipping = await _unitOfWork.ShippingRepository.GetAsync(b => b.Id == id && b.isDeleted);
		shipping.isDeleted = false;
		_unitOfWork.ShippingRepository.Update(shipping);
		int result = await _unitOfWork.SaveAsync();
		if (result is 0)
		{
			return new ErrorResult(Messages.NotRecovered(Messages.Shipping));
		}
		return new SuccessResult(Messages.Recovered(Messages.Shipping));
	}

	#endregion

	#region Delete Requests
	public async Task<IResult> HardDeleteByIdAsync(int id)
    {
        Shipping shipping = await _unitOfWork.ShippingRepository.GetAsync(b => b.Id == id && !b.isDeleted);
        _unitOfWork.ShippingRepository.Delete(shipping);
        int result = await _unitOfWork.SaveAsync();
        if (result is 0)
        {
            return new ErrorResult(Messages.NotDeleted(Messages.Shipping));
        }
        return new SuccessResult(Messages.Deleted(Messages.Shipping));
    }
    public async Task<IResult> SoftDeleteByIdAsync(int id)
    {
        Shipping shipping = await _unitOfWork.ShippingRepository.GetAsync(b => b.Id == id && !b.isDeleted);
        shipping.isDeleted = true;
        _unitOfWork.ShippingRepository.Update(shipping);
        int result = await _unitOfWork.SaveAsync();
        if (result is 0)
        {
            return new ErrorResult(Messages.NotDeleted(Messages.Shipping));
        }
        return new SuccessResult(Messages.Deleted(Messages.Shipping));
    }

	#endregion
}
