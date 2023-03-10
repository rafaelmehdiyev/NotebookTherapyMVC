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

    public async Task<IDataResult<List<SaleGetDto>>> GetAllAsync(params string[] includes)
    {
        List<Sale> sales = await _unitOfWork.SaleRepository.GetAllAsync(p => !p.isDeleted, includes);
        if (sales is null)
        {
            return new ErrorDataResult<List<SaleGetDto>>("Salelar Tapilmadi");
        }
        return new SuccessDataResult<List<SaleGetDto>>(_mapper.Map<List<SaleGetDto>>(sales));
    }

    public async Task<IDataResult<SaleGetDto>> GetByIdAsync(int id, params string[] includes)
    {
        Sale sale = await _unitOfWork.SaleRepository.GetAsync(b => b.Id == id && !b.isDeleted, includes);
        if (sale is null)
        {
            return new ErrorDataResult<SaleGetDto>("Sale Tapilmadi");
        }
        return new SuccessDataResult<SaleGetDto>(_mapper.Map<SaleGetDto>(sale));
    }

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
}
