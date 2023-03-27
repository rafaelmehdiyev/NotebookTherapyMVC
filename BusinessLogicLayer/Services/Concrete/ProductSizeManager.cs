namespace BusinessLogicLayer.Services.Concrete;

public class ProductSizeManager : IProductSizeService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ProductSizeManager(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IDataResult<List<ProductSizeGetDto>>> GetAllAsync(params string[] includes)
    {
        List<ProductSize> productSizes = await _unitOfWork.ProductSizeRepository.GetAllAsync(includes:includes);
        if (productSizes is null)
        {
            return new ErrorDataResult<List<ProductSizeGetDto>>("ProductSizelar Tapilmadi");
        }
        return new SuccessDataResult<List<ProductSizeGetDto>>(_mapper.Map<List<ProductSizeGetDto>>(productSizes));
    }

    public async Task<IDataResult<ProductSizeGetDto>> GetByIdAsync(int id, params string[] includes)
    {
        ProductSize productSize = await _unitOfWork.ProductSizeRepository.GetAsync(b => b.Id == id, includes);
        if (productSize is null)
        {
            return new ErrorDataResult<ProductSizeGetDto>("ProductSize Tapilmadi");
        }
        return new SuccessDataResult<ProductSizeGetDto>(_mapper.Map<ProductSizeGetDto>(productSize));
    }

    public async Task<IResult> CreateAsync(ProductSizePostDto dto)
    {
        ProductSize productSize = _mapper.Map<ProductSize>(dto);
        await _unitOfWork.ProductSizeRepository.CreateAsync(productSize);
        int result = await _unitOfWork.SaveAsync();
        if (result is 0)
        {
            return new ErrorResult("ProductSize Yaradila bilmedi");
        }
        return new SuccessResult("ProductSize Yaradildi");
    }
    public async Task<IResult> UpdateAsync(ProductSizeUpdateDto dto)
    {
        ProductSize productSize = await _unitOfWork.ProductSizeRepository.GetAsync(b => b.Id == dto.Id);
        productSize = _mapper.Map<ProductSize>(dto);
        _unitOfWork.ProductSizeRepository.Update(productSize);
        int result = await _unitOfWork.SaveAsync();
        if (result is 0)
        {
            return new ErrorResult("ProductSize Yenilene bilmedi");
        }
        return new SuccessResult("ProductSize Yenilendi");
    }
    public async Task<IResult> HardDeleteByIdAsync(int id)
    {
        ProductSize productSize = await _unitOfWork.ProductSizeRepository.GetAsync(b => b.Id == id);
        _unitOfWork.ProductSizeRepository.Delete(productSize);
        int result = await _unitOfWork.SaveAsync();
        if (result is 0)
        {
            return new ErrorResult("ProductSize Siline bilmedi");
        }
        return new SuccessResult("ProductSize Silindi");
    }

    public async Task<IResult> SoftDeleteByIdAsync(int id)
    {
        ProductSize productSize = await _unitOfWork.ProductSizeRepository.GetAsync(b => b.Id == id);
        _unitOfWork.ProductSizeRepository.Update(productSize);
        int result = await _unitOfWork.SaveAsync();
        if (result is 0)
        {
            return new ErrorResult("ProductSize Siline bilmedi");
        }
        return new SuccessResult("ProductSize Silindi");
    }
}