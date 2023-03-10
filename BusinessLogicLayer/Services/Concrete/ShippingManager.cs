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

    public async Task<IDataResult<List<ShippingGetDto>>> GetAllAsync(params string[] includes)
    {
        List<Shipping> shippings = await _unitOfWork.ShippingRepository.GetAllAsync(p => !p.isDeleted, includes);
        if (shippings is null)
        {
            return new ErrorDataResult<List<ShippingGetDto>>("Shippinglar Tapilmadi");
        }
        return new SuccessDataResult<List<ShippingGetDto>>(_mapper.Map<List<ShippingGetDto>>(shippings));
    }

    public async Task<IDataResult<ShippingGetDto>> GetByIdAsync(int id, params string[] includes)
    {
        Shipping shipping = await _unitOfWork.ShippingRepository.GetAsync(b => b.Id == id && !b.isDeleted, includes);
        if (shipping is null)
        {
            return new ErrorDataResult<ShippingGetDto>("Shipping Tapilmadi");
        }
        return new SuccessDataResult<ShippingGetDto>(_mapper.Map<ShippingGetDto>(shipping));
    }

    public async Task<IResult> CreateAsync(ShippingPostDto dto)
    {
        Shipping shipping = _mapper.Map<Shipping>(dto);
        await _unitOfWork.ShippingRepository.CreateAsync(shipping);
        int result = await _unitOfWork.SaveAsync();
        if (result is 0)
        {
            return new ErrorResult("Shipping Yaradila bilmedi");
        }
        return new SuccessResult("Shipping Yaradildi");
    }
    public async Task<IResult> UpdateAsync(ShippingUpdateDto dto)
    {
        Shipping shipping = await _unitOfWork.ShippingRepository.GetAsync(b => b.Id == dto.Id && !b.isDeleted);
        shipping = _mapper.Map<Shipping>(dto);
        _unitOfWork.ShippingRepository.Update(shipping);
        int result = await _unitOfWork.SaveAsync();
        if (result is 0)
        {
            return new ErrorResult("Shipping Yenilene bilmedi");
        }
        return new SuccessResult("Shipping Yenilendi");
    }
    public async Task<IResult> HardDeleteByIdAsync(int id)
    {
        Shipping shipping = await _unitOfWork.ShippingRepository.GetAsync(b => b.Id == id && !b.isDeleted);
        _unitOfWork.ShippingRepository.Delete(shipping);
        int result = await _unitOfWork.SaveAsync();
        if (result is 0)
        {
            return new ErrorResult("Shipping Siline bilmedi");
        }
        return new SuccessResult("Shipping Silindi");
    }

    public async Task<IResult> SoftDeleteByIdAsync(int id)
    {
        Shipping shipping = await _unitOfWork.ShippingRepository.GetAsync(b => b.Id == id && !b.isDeleted);
        shipping.isDeleted = true;
        _unitOfWork.ShippingRepository.Update(shipping);
        int result = await _unitOfWork.SaveAsync();
        if (result is 0)
        {
            return new ErrorResult("Shipping Siline bilmedi");
        }
        return new SuccessResult("Shipping Silindi");
    }
}
