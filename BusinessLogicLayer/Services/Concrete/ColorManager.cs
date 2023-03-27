namespace BusinessLogicLayer.Services.Concrete;

public class ColorManager : IColorService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ColorManager(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IDataResult<List<ColorGetDto>>> GetAllAsync(params string[] includes)
    {
        List<Color> sizes = await _unitOfWork.ColorRepository.GetAllAsync(p => !p.isDeleted, includes);
        if (sizes is null)
        {
            return new ErrorDataResult<List<ColorGetDto>>("Colorlar Tapilmadi");
        }
        return new SuccessDataResult<List<ColorGetDto>>(_mapper.Map<List<ColorGetDto>>(sizes));
    }

    public async Task<IDataResult<ColorGetDto>> GetByIdAsync(int id, params string[] includes)
    {
        Color size = await _unitOfWork.ColorRepository.GetAsync(b => b.Id == id && !b.isDeleted, includes);
        if (size is null)
        {
            return new ErrorDataResult<ColorGetDto>("Color Tapilmadi");
        }
        return new SuccessDataResult<ColorGetDto>(_mapper.Map<ColorGetDto>(size));
    }

    public async Task<IResult> CreateAsync(ColorPostDto dto)
    {
        Color size = _mapper.Map<Color>(dto);
        await _unitOfWork.ColorRepository.CreateAsync(size);
        int result = await _unitOfWork.SaveAsync();
        if (result is 0)
        {
            return new ErrorResult("Color Yaradila bilmedi");
        }
        return new SuccessResult("Color Yaradildi");
    }
    public async Task<IResult> UpdateAsync(ColorUpdateDto dto)
    {
        Color size = await _unitOfWork.ColorRepository.GetAsync(b => b.Id == dto.Id && !b.isDeleted);
        size = _mapper.Map<Color>(dto);
        _unitOfWork.ColorRepository.Update(size);
        int result = await _unitOfWork.SaveAsync();
        if (result is 0)
        {
            return new ErrorResult("Color Yenilene bilmedi");
        }
        return new SuccessResult("Color Yenilendi");
    }
    public async Task<IResult> HardDeleteByIdAsync(int id)
    {
        Color size = await _unitOfWork.ColorRepository.GetAsync(b => b.Id == id && !b.isDeleted);
        _unitOfWork.ColorRepository.Delete(size);
        int result = await _unitOfWork.SaveAsync();
        if (result is 0)
        {
            return new ErrorResult("Color Siline bilmedi");
        }
        return new SuccessResult("Color Silindi");
    }

    public async Task<IResult> SoftDeleteByIdAsync(int id)
    {
        Color size = await _unitOfWork.ColorRepository.GetAsync(b => b.Id == id && !b.isDeleted);
        size.isDeleted = true;
        _unitOfWork.ColorRepository.Update(size);
        int result = await _unitOfWork.SaveAsync();
        if (result is 0)
        {
            return new ErrorResult("Color Siline bilmedi");
        }
        return new SuccessResult("Color Silindi");
    }
}