namespace BusinessLogicLayer.Services.Concrete;

public class SaleItemManager : ISaleItemService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public SaleItemManager(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

	#region Get Requests
	public async Task<IDataResult<List<SaleItemGetDto>>> GetAllAsync(bool getDeleted, params string[] includes)
    {
        List<SaleItem> saleItems = getDeleted
            ? await _unitOfWork.SaleItemRepository.GetAllAsync(includes: includes)
            : await _unitOfWork.SaleItemRepository.GetAllAsync(b => !b.isDeleted, includes);
        if (saleItems is null)
        {
            return new ErrorDataResult<List<SaleItemGetDto>>(Messages.NotFound(Messages.SaleItem));
        }
        return new SuccessDataResult<List<SaleItemGetDto>>(_mapper.Map<List<SaleItemGetDto>>(saleItems));
    }
    public async Task<IDataResult<SaleItemGetDto>> GetByIdAsync(int id, params string[] includes)
    {
        SaleItem saleItem = await _unitOfWork.SaleItemRepository.GetAsync(b => b.Id == id, includes);
        if (saleItem is null)
        {
            return new ErrorDataResult<SaleItemGetDto>(Messages.NotFound(Messages.SaleItem));
        }
        return new SuccessDataResult<SaleItemGetDto>(_mapper.Map<SaleItemGetDto>(saleItem));
    }

	#endregion

	#region Post Requests
    public async Task<IResult> CreateAsync(SaleItemPostDto dto)
    {
        SaleItem saleItem = _mapper.Map<SaleItem>(dto);
        await _unitOfWork.SaleItemRepository.CreateAsync(saleItem);
        int result = await _unitOfWork.SaveAsync();
        if (result is 0)
        {
            return new ErrorResult(Messages.NotCreated(Messages.SaleItem));
        }
        return new SuccessResult(Messages.Created(Messages.SaleItem));
    }

	#endregion

	#region Update Requests
    public async Task<IResult> UpdateAsync(SaleItemUpdateDto dto)
    {
        SaleItem saleItem = await _unitOfWork.SaleItemRepository.GetAsync(b => b.Id == dto.Id && !b.isDeleted);
        saleItem = _mapper.Map<SaleItem>(dto);
        _unitOfWork.SaleItemRepository.Update(saleItem);
        int result = await _unitOfWork.SaveAsync();
        if (result is 0)
        {
            return new ErrorResult(Messages.NotUpdated(Messages.SaleItem));
        }
        return new SuccessResult(Messages.Updated(Messages.SaleItem));
    }
	public async Task<IResult> RecoverByIdAsync(int id)
	{
		SaleItem saleItem = await _unitOfWork.SaleItemRepository.GetAsync(b => b.Id == id && b.isDeleted);
		saleItem.isDeleted = false;
		_unitOfWork.SaleItemRepository.Update(saleItem);
		int result = await _unitOfWork.SaveAsync();
		if (result is 0)
		{
			return new ErrorResult(Messages.NotRecovered(Messages.SaleItem));
		}
		return new SuccessResult(Messages.Recovered(Messages.SaleItem));
	}

	#endregion

	#region Delete Requests
	public async Task<IResult> HardDeleteByIdAsync(int id)
    {
        SaleItem saleItem = await _unitOfWork.SaleItemRepository.GetAsync(b => b.Id == id && !b.isDeleted);
        _unitOfWork.SaleItemRepository.Delete(saleItem);
        int result = await _unitOfWork.SaveAsync();
        if (result is 0)
        {
            return new ErrorResult(Messages.NotDeleted(Messages.SaleItem));
        }
        return new SuccessResult(Messages.Deleted(Messages.SaleItem));
    }
    public async Task<IResult> SoftDeleteByIdAsync(int id)
    {
        SaleItem saleItem = await _unitOfWork.SaleItemRepository.GetAsync(b => b.Id == id && !b.isDeleted);
        saleItem.isDeleted = true;
        _unitOfWork.SaleItemRepository.Update(saleItem);
        int result = await _unitOfWork.SaveAsync();
        if (result is 0)
        {
            return new ErrorResult(Messages.NotDeleted(Messages.SaleItem));
        }
        return new SuccessResult(Messages.Deleted(Messages.SaleItem));
    }

	#endregion
}
