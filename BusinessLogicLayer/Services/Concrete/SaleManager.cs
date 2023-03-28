namespace BusinessLogicLayer.Services.Concrete;

public class SaleManager : ISaleService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public SaleManager(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

	#region Get Requests
	public async Task<IDataResult<List<SaleGetDto>>> GetAllAsync(params string[] includes)
    {
        List<Sale> sales = await _unitOfWork.SaleRepository.GetAllAsync(includes: includes);
        if (sales is null)
        {
            return new ErrorDataResult<List<SaleGetDto>>("Salelar Tapilmadi");
        }
        return new SuccessDataResult<List<SaleGetDto>>(_mapper.Map<List<SaleGetDto>>(sales));
    }
    public async Task<IDataResult<SaleGetDto>> GetByIdAsync(int id, params string[] includes)
    {
        Sale sale = await _unitOfWork.SaleRepository.GetAsync(b => b.Id == id, includes);
        if (sale is null)
        {
            return new ErrorDataResult<SaleGetDto>("Sale Tapilmadi");
        }
        return new SuccessDataResult<SaleGetDto>(_mapper.Map<SaleGetDto>(sale));
    }

	#endregion

	#region Post Requests
    public async Task<IResult> CreateAsync(SalePostDto dto)
    {
        Sale sale = _mapper.Map<Sale>(dto);
        await _unitOfWork.SaleRepository.CreateAsync(sale);
        int result = await _unitOfWork.SaveAsync();
        if (result is 0)
        {
            return new ErrorResult("Sale Yaradila bilmedi");
        }
        return new SuccessResult("Sale Yaradildi");
    }

	#endregion

	#region Update Requests
    public async Task<IResult> UpdateAsync(SaleUpdateDto dto)
    {
        Sale sale = await _unitOfWork.SaleRepository.GetAsync(b => b.Id == dto.Id && !b.isDeleted);
        sale = _mapper.Map<Sale>(dto);
        _unitOfWork.SaleRepository.Update(sale);
        int result = await _unitOfWork.SaveAsync();
        if (result is 0)
        {
            return new ErrorResult("Sale Yenilene bilmedi");
        }
        return new SuccessResult("Sale Yenilendi");
    }
	public async Task<IResult> RecoverByIdAsync(int id)
	{
		Sale sale = await _unitOfWork.SaleRepository.GetAsync(b => b.Id == id && b.isDeleted);
		sale.isDeleted = false;
		_unitOfWork.SaleRepository.Update(sale);
		int result = await _unitOfWork.SaveAsync();
		if (result is 0)
		{
			return new ErrorResult("Sale Siline bilmedi");
		}
		return new SuccessResult("Sale Silindi");
	}

	#endregion

	#region Delete Requests
	public async Task<IResult> HardDeleteByIdAsync(int id)
    {
        Sale sale = await _unitOfWork.SaleRepository.GetAsync(b => b.Id == id && !b.isDeleted);
        _unitOfWork.SaleRepository.Delete(sale);
        int result = await _unitOfWork.SaveAsync();
        if (result is 0)
        {
            return new ErrorResult("Sale Siline bilmedi");
        }
        return new SuccessResult("Sale Silindi");
    }
    public async Task<IResult> SoftDeleteByIdAsync(int id)
    {
        Sale sale = await _unitOfWork.SaleRepository.GetAsync(b => b.Id == id && !b.isDeleted);
        sale.isDeleted = true;
        _unitOfWork.SaleRepository.Update(sale);
        int result = await _unitOfWork.SaveAsync();
        if (result is 0)
        {
            return new ErrorResult("Sale Siline bilmedi");
        }
        return new SuccessResult("Sale Silindi");
    }

	#endregion
}
