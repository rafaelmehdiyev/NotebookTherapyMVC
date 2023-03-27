namespace BusinessLogicLayer.Services.Concrete;

public class BundleManager : IBundleService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public BundleManager(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IDataResult<List<BundleGetDto>>> GetAllAsync(params string[] includes)
    {
        List<Bundle> bundles = await _unitOfWork.BundleRepository.GetAllAsync(p => !p.isDeleted, includes);
        if (bundles is null)
        {
            return new ErrorDataResult<List<BundleGetDto>>("Bundlelar Tapilmadi");
        }
        return new SuccessDataResult<List<BundleGetDto>>(_mapper.Map<List<BundleGetDto>>(bundles));
    }

    public async Task<IDataResult<BundleGetDto>> GetByIdAsync(int id, params string[] includes)
    {
        Bundle bundle = await _unitOfWork.BundleRepository.GetAsync(b => b.Id == id && !b.isDeleted, includes);
        if (bundle is null)
        {
            return new ErrorDataResult<BundleGetDto>("Bundle Tapilmadi");
        }
        return new SuccessDataResult<BundleGetDto>(_mapper.Map<BundleGetDto>(bundle));
    }

    public async Task<IResult> CreateAsync(BundlePostDto dto)
    {
        Bundle bundle = _mapper.Map<Bundle>(dto);
        await _unitOfWork.BundleRepository.CreateAsync(bundle);
        int result = await _unitOfWork.SaveAsync();
        if (result is 0)
        {
            return new ErrorResult("Bundle Yaradila bilmedi");
        }
        return new SuccessResult("Bundle Yaradildi");
    }
    public async Task<IResult> UpdateAsync(BundleUpdateDto dto)
    {
        Bundle bundle = await _unitOfWork.BundleRepository.GetAsync(b => b.Id == dto.Id && !b.isDeleted);
        bundle = _mapper.Map<Bundle>(dto);
        _unitOfWork.BundleRepository.Update(bundle);
        int result = await _unitOfWork.SaveAsync();
        if (result is 0)
        {
            return new ErrorResult("Bundle Yenilene bilmedi");
        }
        return new SuccessResult("Bundle Yenilendi");
    }
    public async Task<IResult> HardDeleteByIdAsync(int id)
    {
        Bundle bundle = await _unitOfWork.BundleRepository.GetAsync(b => b.Id == id && !b.isDeleted);
        _unitOfWork.BundleRepository.Delete(bundle);
        int result = await _unitOfWork.SaveAsync();
        if (result is 0)
        {
            return new ErrorResult("Bundle Siline bilmedi");
        }
        return new SuccessResult("Bundle Silindi");
    }

    public async Task<IResult> SoftDeleteByIdAsync(int id)
    {
        Bundle bundle = await _unitOfWork.BundleRepository.GetAsync(b => b.Id == id && !b.isDeleted);
        bundle.isDeleted = true;
        _unitOfWork.BundleRepository.Update(bundle);
        int result = await _unitOfWork.SaveAsync();
        if (result is 0)
        {
            return new ErrorResult("Bundle Siline bilmedi");
        }
        return new SuccessResult("Bundle Silindi");
    }
}