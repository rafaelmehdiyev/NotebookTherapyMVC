namespace BusinessLogicLayer.Services.Concrete;

public class ProductManager : IProductService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ProductManager(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IDataResult<List<ProductGetDto>>> GetAllAsync(params string[] includes)
    {
        List<Product> products = await _unitOfWork.ProductRepository.GetAllAsync(p => !p.isDeleted, includes);
        if (products is null)
        {
            return new ErrorDataResult<List<ProductGetDto>>("Productlar Tapilmadi");
        }
        return new SuccessDataResult<List<ProductGetDto>>(_mapper.Map<List<ProductGetDto>>(products));
    }

    public async Task<IDataResult<ProductGetDto>> GetByIdAsync(int id, params string[] includes)
    {
        Product product = await _unitOfWork.ProductRepository.GetAsync(b => b.Id == id && !b.isDeleted, includes);
        if (product is null)
        {
            return new ErrorDataResult<ProductGetDto>("Product Tapilmadi");
        }
        return new SuccessDataResult<ProductGetDto>(_mapper.Map<ProductGetDto>(product));
    }

    public async Task<IResult> CreateAsync(ProductPostDto dto)
    {
        Product product = _mapper.Map<Product>(dto);
        await _unitOfWork.ProductRepository.CreateAsync(product);
        int result = await _unitOfWork.SaveAsync();
        if (result is 0)
        {
            return new ErrorResult("Product Yaradila bilmedi");
        }
        return new SuccessResult("Product Yaradildi");
    }
    public async Task<IResult> UpdateAsync(ProductUpdateDto dto)
    {
        Product product = await _unitOfWork.ProductRepository.GetAsync(b => b.Id == dto.Id && !b.isDeleted);
        product = _mapper.Map<Product>(dto);
        _unitOfWork.ProductRepository.Update(product);
        int result = await _unitOfWork.SaveAsync();
        if (result is 0)
        {
            return new ErrorResult("Product Yenilene bilmedi");
        }
        return new SuccessResult("Product Yenilendi");
    }
    public async Task<IResult> HardDeleteByIdAsync(int id)
    {
        Product product = await _unitOfWork.ProductRepository.GetAsync(b => b.Id == id && !b.isDeleted);
        _unitOfWork.ProductRepository.Delete(product);
        int result = await _unitOfWork.SaveAsync();
        if (result is 0)
        {
            return new ErrorResult("Product Siline bilmedi");
        }
        return new SuccessResult("Product Silindi");
    }

    public async Task<IResult> SoftDeleteByIdAsync(int id)
    {
        Product product = await _unitOfWork.ProductRepository.GetAsync(b => b.Id == id && !b.isDeleted);
        product.isDeleted = true;
        _unitOfWork.ProductRepository.Update(product);
        int result = await _unitOfWork.SaveAsync();
        if (result is 0)
        {
            return new ErrorResult("Product Siline bilmedi");
        }
        return new SuccessResult("Product Silindi");
    }
}
