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
	public async Task<IDataResult<List<SaleItemGetDto>>> GetAllAsync(params string[] includes)
    {
        List<SaleItem> saleItems = await _unitOfWork.SaleItemRepository.GetAllAsync(includes: includes);
        if (saleItems is null)
        {
            return new ErrorDataResult<List<SaleItemGetDto>>("SaleItemlar Tapilmadi");
        }
        return new SuccessDataResult<List<SaleItemGetDto>>(_mapper.Map<List<SaleItemGetDto>>(saleItems));
    }
    public async Task<IDataResult<SaleItemGetDto>> GetByIdAsync(int id, params string[] includes)
    {
        SaleItem saleItem = await _unitOfWork.SaleItemRepository.GetAsync(b => b.Id == id, includes);
        if (saleItem is null)
        {
            return new ErrorDataResult<SaleItemGetDto>("SaleItem Tapilmadi");
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
            return new ErrorResult("SaleItem Yaradila bilmedi");
        }
        return new SuccessResult("SaleItem Yaradildi");
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
            return new ErrorResult("SaleItem Yenilene bilmedi");
        }
        return new SuccessResult("SaleItem Yenilendi");
    }
	public async Task<IResult> RecoverByIdAsync(int id)
	{
		SaleItem saleItem = await _unitOfWork.SaleItemRepository.GetAsync(b => b.Id == id && b.isDeleted);
		saleItem.isDeleted = false;
		_unitOfWork.SaleItemRepository.Update(saleItem);
		int result = await _unitOfWork.SaveAsync();
		if (result is 0)
		{
			return new ErrorResult("SaleItem is not recovered");
		}
		return new SuccessResult("SaleItem is recovered");
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
            return new ErrorResult("SaleItem Siline bilmedi");
        }
        return new SuccessResult("SaleItem Silindi");
    }
    public async Task<IResult> SoftDeleteByIdAsync(int id)
    {
        SaleItem saleItem = await _unitOfWork.SaleItemRepository.GetAsync(b => b.Id == id && !b.isDeleted);
        saleItem.isDeleted = true;
        _unitOfWork.SaleItemRepository.Update(saleItem);
        int result = await _unitOfWork.SaveAsync();
        if (result is 0)
        {
            return new ErrorResult("SaleItem Siline bilmedi");
        }
        return new SuccessResult("SaleItem Silindi");
    }

	#endregion
}
