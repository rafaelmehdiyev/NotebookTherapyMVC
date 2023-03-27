namespace BusinessLogicLayer.Services.Concrete;
public class SizeManager : ISizeService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public SizeManager(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IDataResult<List<SizeGetDto>>> GetAllAsync(params string[] includes)
    {
        List<Size> sizes = await _unitOfWork.SizeRepository.GetAllAsync(p => !p.isDeleted, includes);
        if (sizes is null)
        {
            return new ErrorDataResult<List<SizeGetDto>>("Sizelar Tapilmadi");
        }
        return new SuccessDataResult<List<SizeGetDto>>(_mapper.Map<List<SizeGetDto>>(sizes));
    }

    public async Task<IDataResult<SizeGetDto>> GetByIdAsync(int id, params string[] includes)
    {
        Size size = await _unitOfWork.SizeRepository.GetAsync(b => b.Id == id && !b.isDeleted, includes);
        if (size is null)
        {
            return new ErrorDataResult<SizeGetDto>("Size Tapilmadi");
        }
        return new SuccessDataResult<SizeGetDto>(_mapper.Map<SizeGetDto>(size));
    }

    public async Task<IResult> CreateAsync(SizePostDto dto)
    {
        Size size = _mapper.Map<Size>(dto);
        await _unitOfWork.SizeRepository.CreateAsync(size);
        int result = await _unitOfWork.SaveAsync();
        if (result is 0)
        {
            return new ErrorResult("Size Yaradila bilmedi");
        }
        return new SuccessResult("Size Yaradildi");
    }
    public async Task<IResult> UpdateAsync(SizeUpdateDto dto)
    {
        Size size = await _unitOfWork.SizeRepository.GetAsync(b => b.Id == dto.Id && !b.isDeleted);
        size = _mapper.Map<Size>(dto);
        _unitOfWork.SizeRepository.Update(size);
        int result = await _unitOfWork.SaveAsync();
        if (result is 0)
        {
            return new ErrorResult("Size Yenilene bilmedi");
        }
        return new SuccessResult("Size Yenilendi");
    }
    public async Task<IResult> HardDeleteByIdAsync(int id)
    {
        Size size = await _unitOfWork.SizeRepository.GetAsync(b => b.Id == id && !b.isDeleted);
        _unitOfWork.SizeRepository.Delete(size);
        int result = await _unitOfWork.SaveAsync();
        if (result is 0)
        {
            return new ErrorResult("Size Siline bilmedi");
        }
        return new SuccessResult("Size Silindi");
    }

    public async Task<IResult> SoftDeleteByIdAsync(int id)
    {
        Size size = await _unitOfWork.SizeRepository.GetAsync(b => b.Id == id && !b.isDeleted);
        size.isDeleted = true;
        _unitOfWork.SizeRepository.Update(size);
        int result = await _unitOfWork.SaveAsync();
        if (result is 0)
        {
            return new ErrorResult("Size Siline bilmedi");
        }
        return new SuccessResult("Size Silindi");
    }
}
