namespace BusinessLogicLayer.Services.Concrete;

public class CartManager : ICartService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CartManager(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IDataResult<List<CartGetDto>>> GetAllAsync(params string[] includes)
    {
        List<Cart> carts = await _unitOfWork.CartRepository.GetAllAsync(p => !p.isDeleted, includes);
        if (carts is null)
        {
            return new ErrorDataResult<List<CartGetDto>>("Cartlar Tapilmadi");
        }
        return new SuccessDataResult<List<CartGetDto>>(_mapper.Map<List<CartGetDto>>(carts));
    }

    public async Task<IDataResult<CartGetDto>> GetByIdAsync(int id, params string[] includes)
    {
        Cart cart = await _unitOfWork.CartRepository.GetAsync(b => b.Id == id && !b.isDeleted, includes);
        if (cart is null)
        {
            return new ErrorDataResult<CartGetDto>("Cart Tapilmadi");
        }
        return new SuccessDataResult<CartGetDto>(_mapper.Map<CartGetDto>(cart));
    }

    public async Task<IResult> CreateAsync(CartPostDto dto)
    {
        Cart cart = _mapper.Map<Cart>(dto);
        await _unitOfWork.CartRepository.CreateAsync(cart);
        int result = await _unitOfWork.SaveAsync();
        if (result is 0)
        {
            return new ErrorResult("Cart Yaradila bilmedi");
        }
        return new SuccessResult("Cart Yaradildi");
    }
    public async Task<IResult> UpdateAsync(CartUpdateDto dto)
    {
        Cart cart = await _unitOfWork.CartRepository.GetAsync(b => b.Id == dto.Id && !b.isDeleted);
        cart = _mapper.Map<Cart>(dto);
        _unitOfWork.CartRepository.Update(cart);
        int result = await _unitOfWork.SaveAsync();
        if (result is 0)
        {
            return new ErrorResult("Cart Yenilene bilmedi");
        }
        return new SuccessResult("Cart Yenilendi");
    }
    public async Task<IResult> HardDeleteByIdAsync(int id)
    {
        Cart cart = await _unitOfWork.CartRepository.GetAsync(b => b.Id == id && !b.isDeleted);
        _unitOfWork.CartRepository.Delete(cart);
        int result = await _unitOfWork.SaveAsync();
        if (result is 0)
        {
            return new ErrorResult("Cart Siline bilmedi");
        }
        return new SuccessResult("Cart Silindi");
    }

    public async Task<IResult> SoftDeleteByIdAsync(int id)
    {
        Cart cart = await _unitOfWork.CartRepository.GetAsync(b => b.Id == id && !b.isDeleted);
        cart.isDeleted = true;
        _unitOfWork.CartRepository.Update(cart);
        int result = await _unitOfWork.SaveAsync();
        if (result is 0)
        {
            return new ErrorResult("Cart Siline bilmedi");
        }
        return new SuccessResult("Cart Silindi");
    }
}


