using System;
namespace BusinessLogicLayer.Services.Concrete;

public class ProductManager : IProductService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IWebHostEnvironment _env;
    public ProductManager(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment env)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _env = env;
    }

    #region Get Requests
    public async Task<IDataResult<List<ProductGetDto>>> GetAllAsync(bool getDeleted, params string[] includes)
    {
        List<Product> products = getDeleted
            ? await _unitOfWork.ProductRepository.GetAllAsync(includes: includes)
            : await _unitOfWork.ProductRepository.GetAllAsync(b => !b.isDeleted, includes);
        if (products is null)
        {
            return new ErrorDataResult<List<ProductGetDto>>(Messages.NotFound(Messages.Product));
        }
        return new SuccessDataResult<List<ProductGetDto>>(_mapper.Map<List<ProductGetDto>>(products.OrderByDescending(p=>p.CreatedDate).ToList()));
    }
    public async Task<IDataResult<ProductGetDto>> GetByIdAsync(int id, params string[] includes)
    {
        Product product = await _unitOfWork.ProductRepository.GetAsync(b => b.Id == id, includes);
        if (product is null)
        {
            return new ErrorDataResult<ProductGetDto>(Messages.NotFound(Messages.Product));
        }
        return new SuccessDataResult<ProductGetDto>(_mapper.Map<ProductGetDto>(product));
    }
    #endregion

    #region Post Requests
    public async Task<IResult> CreateAsync(ProductPostDto dto)
    {
        Product product = _mapper.Map<Product>(dto);
        FillProduct(product, dto.formFiles, dto.SizeIds, dto.BundlesIds);
        await _unitOfWork.ProductRepository.CreateAsync(product);
        int result = await _unitOfWork.SaveAsync();
        if (result is 0)
        {
            return new ErrorResult(Messages.NotCreated(Messages.Product));
        }
        return new SuccessResult(Messages.Created(Messages.Product));
    }
    #endregion

    #region Update Requests
    public async Task<IResult> UpdateAsync(ProductUpdateDto dto)
    {
        Product product = await _unitOfWork.ProductRepository.GetAsync(b => b.Id == dto.Id && !b.isDeleted, "ProductImages", "ProductSizes.Size", "ProductBundles.Bundle","Reviews");
        if (product.ProductSizes.ToList().Count != 0)
        {
            foreach (ProductSize size in product.ProductSizes.ToList())
            {
                _unitOfWork.ProductSizeRepository.Delete(size);
                await _unitOfWork.SaveAsync();
                product.ProductSizes.Remove(size);
            }
        }
        if (product.ProductBundles.ToList().Count != 0)
        {
            foreach (ProductBundle bundle in product.ProductBundles.ToList())
            {
                _unitOfWork.ProductBundleRepository.Delete(bundle);
                await _unitOfWork.SaveAsync();
                product.ProductBundles.Remove(bundle);
            }
        }
        if (product.ProductImages.ToList().Count != 0)
        {
            foreach (ProductImage image in product.ProductImages.ToList())
            {
                File.Delete(Path.Combine(_env.WebRootPath, "uploads/product", image.ImagePath));
                _unitOfWork.ProductImageRepository.Delete(image);
                await _unitOfWork.SaveAsync();
                product.ProductImages.Remove(image);
            }
        }
        dto.TotalRating = product.Reviews is not null ? (decimal)product.Reviews.Average(r => (int)r.Rating) : 0;
        product = _mapper.Map<Product>(dto);
        product.isSale = product.DiscountPercent > 0 ? true : false;
        FillProduct(product, dto.formFiles, dto.SizeIds, dto.BundlesIds);
        _unitOfWork.ProductRepository.Update(product);
        int result = await _unitOfWork.SaveAsync();
        if (result is 0)
        {
            return new ErrorResult(Messages.NotUpdated(Messages.Product));
        }
        return new SuccessResult(Messages.Updated(Messages.Product));
    }
	public async Task<IResult> RecoverByIdAsync(int id)
	{
		Product product = await _unitOfWork.ProductRepository.GetAsync(b => b.Id == id && b.isDeleted);
		product.isDeleted = false;
		_unitOfWork.ProductRepository.Update(product);
		int result = await _unitOfWork.SaveAsync();
		if (result is 0)
		{
			return new ErrorResult(Messages.NotRecovered(Messages.Product));
		}
		return new SuccessResult(Messages.Recovered(Messages.Product));
	}
	#endregion

	#region Delete Requests
	public async Task<IResult> HardDeleteByIdAsync(int id)
    {
        Product product = await _unitOfWork.ProductRepository.GetAsync(b => b.Id == id && b.isDeleted);
        _unitOfWork.ProductRepository.Delete(product);
        int result = await _unitOfWork.SaveAsync();
        if (result is 0)
        {
            return new ErrorResult(Messages.NotDeleted(Messages.Product));
        }
        return new SuccessResult(Messages.Deleted(Messages.Product));
    }
    public async Task<IResult> SoftDeleteByIdAsync(int id)
    {
        Product product = await _unitOfWork.ProductRepository.GetAsync(b => b.Id == id && !b.isDeleted);
        product.isDeleted = true;
        _unitOfWork.ProductRepository.Update(product);
        int result = await _unitOfWork.SaveAsync();
        if (result is 0)
        {
            return new ErrorResult(Messages.NotDeleted(Messages.Product));
        }
        return new SuccessResult(Messages.Deleted(Messages.Product));
    }
    #endregion

    #region Private Methods
    private async void FillProduct(Product product, List<IFormFile> files, List<int> SizeIds, List<int> BundlesIds)
    {
        if (files is not null)
        {
            AddProductImages(product, files);
        }
        if (SizeIds.Count > 0)
        {
            AddSizes(product, SizeIds);
        }
        if (BundlesIds.Count > 0)
        {
            AddBundles(product, BundlesIds);
        }
    }
    private async void AddProductImages(Product product, List<IFormFile> files)
    {
        foreach (IFormFile file in files)
        {
            ProductImage image = new()
            {
                Product = product,
                ImagePath = file.FileCreate(_env.WebRootPath, "uploads/product")
            };
            product.ProductImages.Add(image);
        }
    }

    private async void AddSizes(Product product, List<int> SizeIds)
    {
        foreach (int sizeId in SizeIds)
        {
            ProductSize productSize = new()
            {
                Product = product,
                SizeId = sizeId
            };
            product.ProductSizes.Add(productSize);
        }
    }
    private async void AddBundles(Product product, List<int> BundlesIds)
    {
        foreach (int bundleId in BundlesIds)
        {
            ProductBundle productBundle = new()
            {
                Product = product,
                BundleId = bundleId
            };
            product.ProductBundles.Add(productBundle);
        }
    }
    #endregion
}