namespace BusinessLogicLayer.Services.Concrete;

public class ProductCollectionManager : IProductCollectionService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ProductCollectionManager(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IDataResult<List<ProductCollectionGetDto>>> GetAllAsync(params string[] includes)
    {
        List<ProductCollection> productCollections = await _unitOfWork.ProductCollectionRepository.GetAllAsync(p => !p.isDeleted, includes);
        if (productCollections is null)
        {
            return new ErrorDataResult<List<ProductCollectionGetDto>>("ProductCollectionlar Tapilmadi");
        }
        return new SuccessDataResult<List<ProductCollectionGetDto>>(_mapper.Map<List<ProductCollectionGetDto>>(productCollections));
    }

    public async Task<IDataResult<ProductCollectionGetDto>> GetByIdAsync(int id, params string[] includes)
    {
        ProductCollection productCollection = await _unitOfWork.ProductCollectionRepository.GetAsync(b => b.Id == id && !b.isDeleted, includes);
        if (productCollection is null)
        {
            return new ErrorDataResult<ProductCollectionGetDto>("ProductCollection Tapilmadi");
        }
        return new SuccessDataResult<ProductCollectionGetDto>(_mapper.Map<ProductCollectionGetDto>(productCollection));
    }

    public async Task<IResult> CreateAsync(ProductCollectionPostDto dto)
    {
        ProductCollection productCollection = _mapper.Map<ProductCollection>(dto);
        await _unitOfWork.ProductCollectionRepository.CreateAsync(productCollection);
        int result = await _unitOfWork.SaveAsync();
        if (result is 0)
        {
            return new ErrorResult("ProductCollection Yaradila bilmedi");
        }
        return new SuccessResult("ProductCollection Yaradildi");
    }
    public async Task<IResult> UpdateAsync(ProductCollectionUpdateDto dto)
    {
        ProductCollection productCollection = await _unitOfWork.ProductCollectionRepository.GetAsync(b => b.Id == dto.Id && !b.isDeleted);
        productCollection = _mapper.Map<ProductCollection>(dto);
        _unitOfWork.ProductCollectionRepository.Update(productCollection);
        int result = await _unitOfWork.SaveAsync();
        if (result is 0)
        {
            return new ErrorResult("ProductCollection Yenilene bilmedi");
        }
        return new SuccessResult("ProductCollection Yenilendi");
    }
    public async Task<IResult> HardDeleteByIdAsync(int id)
    {
        ProductCollection productCollection = await _unitOfWork.ProductCollectionRepository.GetAsync(b => b.Id == id && !b.isDeleted);
        _unitOfWork.ProductCollectionRepository.Delete(productCollection);
        int result = await _unitOfWork.SaveAsync();
        if (result is 0)
        {
            return new ErrorResult("ProductCollection Siline bilmedi");
        }
        return new SuccessResult("ProductCollection Silindi");
    }

    public async Task<IResult> SoftDeleteByIdAsync(int id)
    {
        ProductCollection productCollection = await _unitOfWork.ProductCollectionRepository.GetAsync(b => b.Id == id && !b.isDeleted);
        productCollection.isDeleted = true;
        _unitOfWork.ProductCollectionRepository.Update(productCollection);
        int result = await _unitOfWork.SaveAsync();
        if (result is 0)
        {
            return new ErrorResult("ProductCollection Siline bilmedi");
        }
        return new SuccessResult("ProductCollection Silindi");
    }
}


