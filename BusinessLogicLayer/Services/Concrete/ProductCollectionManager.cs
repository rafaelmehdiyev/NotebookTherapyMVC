namespace BusinessLogicLayer.Services.Concrete;

public class ProductCollectionManager : IProductCollectionService
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;
	private readonly IWebHostEnvironment _env;

	public ProductCollectionManager(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment env)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
		_env = env;
	}

	#region Get Requests
	public async Task<IDataResult<List<ProductCollectionGetDto>>> GetAllAsync(bool getDeleted, params string[] includes)
	{
		List<ProductCollection> productCollections = getDeleted
			? await _unitOfWork.ProductCollectionRepository.GetAllAsync(includes: includes)
			: await _unitOfWork.ProductCollectionRepository.GetAllAsync(b => !b.isDeleted, includes);
		if (productCollections is null)
		{
			return new ErrorDataResult<List<ProductCollectionGetDto>>(Messages.NotFound(Messages.ProductCollection));
		}
		return new SuccessDataResult<List<ProductCollectionGetDto>>(_mapper.Map<List<ProductCollectionGetDto>>(productCollections));
	}
	public async Task<IDataResult<ProductCollectionGetDto>> GetByIdAsync(int id, params string[] includes)
	{
		ProductCollection productCollection = await _unitOfWork.ProductCollectionRepository.GetAsync(b => b.Id == id, includes);
		if (productCollection is null)
		{
			return new ErrorDataResult<ProductCollectionGetDto>(Messages.NotFound(Messages.ProductCollection));
		}
		return new SuccessDataResult<ProductCollectionGetDto>(_mapper.Map<ProductCollectionGetDto>(productCollection));
	}
	#endregion

	#region Post Requests
	public async Task<IResult> CreateAsync(ProductCollectionPostDto dto)
	{
		ProductCollection productCollection = _mapper.Map<ProductCollection>(dto);
		AddImagesToCollection(productCollection, dto.CollectionHeaderImage, dto.CollectionFooterImage, dto.CollectionItemBackgroundImage);
		await _unitOfWork.ProductCollectionRepository.CreateAsync(productCollection);
		int result = await _unitOfWork.SaveAsync();
		if (result is 0)
		{
			return new ErrorResult(Messages.NotCreated(Messages.ProductCollection));
		}
		return new SuccessResult(Messages.Created(Messages.ProductCollection));
	}

	#endregion

	#region Update Requests
	public async Task<IResult> UpdateAsync(ProductCollectionUpdateDto dto)
	{
		ProductCollection productCollection = await _unitOfWork.ProductCollectionRepository.GetAsync(b => b.Id == dto.Id && !b.isDeleted);
		DeleteImagesFromCollection(productCollection);
		productCollection = _mapper.Map<ProductCollection>(dto);
		AddImagesToCollection(productCollection, dto.CollectionHeaderImage, dto.CollectionFooterImage, dto.CollectionItemBackgroundImage);
		_unitOfWork.ProductCollectionRepository.Update(productCollection);
		int result = await _unitOfWork.SaveAsync();
		if (result is 0)
		{
			return new ErrorResult(Messages.NotUpdated(Messages.ProductCollection));
		}
		return new SuccessResult(Messages.Updated(Messages.ProductCollection));
	}
	public async Task<IResult> RecoverByIdAsync(int id)
	{
		ProductCollection productCollection = await _unitOfWork.ProductCollectionRepository.GetAsync(b => b.Id == id && b.isDeleted);
		productCollection.isDeleted = false;
		_unitOfWork.ProductCollectionRepository.Update(productCollection);
		int result = await _unitOfWork.SaveAsync();
		if (result is 0)
		{
			return new ErrorResult(Messages.NotRecovered(Messages.ProductCollection));
		}
		return new SuccessResult(Messages.Recovered(Messages.ProductCollection));
	}

	#endregion

	#region Delete Requests
	public async Task<IResult> HardDeleteByIdAsync(int id)
	{
		ProductCollection productCollection = await _unitOfWork.ProductCollectionRepository.GetAsync(b => b.Id == id && !b.isDeleted);
		_unitOfWork.ProductCollectionRepository.Delete(productCollection);
		int result = await _unitOfWork.SaveAsync();
		if (result is 0)
		{
			return new ErrorResult(Messages.NotDeleted(Messages.ProductCollection));
		}
		return new SuccessResult(Messages.Deleted(Messages.ProductCollection));
	}
	public async Task<IResult> SoftDeleteByIdAsync(int id)
	{
		ProductCollection productCollection = await _unitOfWork.ProductCollectionRepository.GetAsync(b => b.Id == id && !b.isDeleted);
		productCollection.isDeleted = true;
		_unitOfWork.ProductCollectionRepository.Update(productCollection);
		int result = await _unitOfWork.SaveAsync();
		if (result is 0)
		{
			return new ErrorResult(Messages.NotDeleted(Messages.ProductCollection));
		}
		return new SuccessResult(Messages.Deleted(Messages.ProductCollection));
	}

	#endregion

	#region Private Methods
	private void AddImagesToCollection(ProductCollection collection, params IFormFile[] formFiles)
	{
		collection.CollectionHeaderImage = formFiles[0].FileCreate(_env.WebRootPath, "uploads/productCollection");
		collection.CollectionFooterImage = formFiles[1].FileCreate(_env.WebRootPath, "uploads/productCollection");
		collection.CollectionItemBackgroundImage = formFiles[2].FileCreate(_env.WebRootPath, "uploads/productCollection");
	}
	private void DeleteImagesFromCollection(ProductCollection collection)
	{
		File.Delete(Path.Combine(_env.WebRootPath, "uploads/productCollection", collection.CollectionHeaderImage));
		File.Delete(Path.Combine(_env.WebRootPath, "uploads/productCollection", collection.CollectionItemBackgroundImage));
		File.Delete(Path.Combine(_env.WebRootPath, "uploads/productCollection", collection.CollectionFooterImage));
	}
	#endregion
}