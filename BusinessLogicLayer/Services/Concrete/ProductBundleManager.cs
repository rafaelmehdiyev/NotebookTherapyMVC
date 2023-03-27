namespace BusinessLogicLayer.Services.Concrete;

public class ProductBundleManager : IProductBundleService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ProductBundleManager(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IDataResult<List<ProductBundleGetDto>>> GetAllAsync(params string[] includes)
    {
        List<ProductBundle> productBundles = await _unitOfWork.ProductBundleRepository.GetAllAsync(includes: includes);
        if (productBundles is null)
        {
            return new ErrorDataResult<List<ProductBundleGetDto>>("ProductBundlelar Tapilmadi");
        }
        return new SuccessDataResult<List<ProductBundleGetDto>>(_mapper.Map<List<ProductBundleGetDto>>(productBundles));
    }

    public async Task<IDataResult<ProductBundleGetDto>> GetByIdAsync(int id, params string[] includes)
    {
        ProductBundle productBundle = await _unitOfWork.ProductBundleRepository.GetAsync(b => b.Id == id, includes);
        if (productBundle is null)
        {
            return new ErrorDataResult<ProductBundleGetDto>("ProductBundle Tapilmadi");
        }
        return new SuccessDataResult<ProductBundleGetDto>(_mapper.Map<ProductBundleGetDto>(productBundle));
    }

    public async Task<IResult> CreateAsync(ProductBundlePostDto dto)
    {
        ProductBundle productBundle = _mapper.Map<ProductBundle>(dto);
        await _unitOfWork.ProductBundleRepository.CreateAsync(productBundle);
        int result = await _unitOfWork.SaveAsync();
        if (result is 0)
        {
            return new ErrorResult("ProductBundle Yaradila bilmedi");
        }
        return new SuccessResult("ProductBundle Yaradildi");
    }
    public async Task<IResult> UpdateAsync(ProductBundleUpdateDto dto)
    {
        ProductBundle productBundle = await _unitOfWork.ProductBundleRepository.GetAsync(b => b.Id == dto.Id);
        productBundle = _mapper.Map<ProductBundle>(dto);
        _unitOfWork.ProductBundleRepository.Update(productBundle);
        int result = await _unitOfWork.SaveAsync();
        if (result is 0)
        {
            return new ErrorResult("ProductBundle Yenilene bilmedi");
        }
        return new SuccessResult("ProductBundle Yenilendi");
    }
    public async Task<IResult> HardDeleteByIdAsync(int id)
    {
        ProductBundle productBundle = await _unitOfWork.ProductBundleRepository.GetAsync(b => b.Id == id);
        _unitOfWork.ProductBundleRepository.Delete(productBundle);
        int result = await _unitOfWork.SaveAsync();
        if (result is 0)
        {
            return new ErrorResult("ProductBundle Siline bilmedi");
        }
        return new SuccessResult("ProductBundle Silindi");
    }

    public async Task<IResult> SoftDeleteByIdAsync(int id)
    {
        ProductBundle productBundle = await _unitOfWork.ProductBundleRepository.GetAsync(b => b.Id == id);
        _unitOfWork.ProductBundleRepository.Update(productBundle);
        int result = await _unitOfWork.SaveAsync();
        if (result is 0)
        {
            return new ErrorResult("ProductBundle Siline bilmedi");
        }
        return new SuccessResult("ProductBundle Silindi");
    }
}