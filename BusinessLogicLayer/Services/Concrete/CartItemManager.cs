namespace BusinessLogicLayer.Services.Concrete;

public class CartItemManager : ICartItemService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CartItemManager(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IDataResult<List<CartItemGetDto>>> GetAllAsync(params string[] includes)
    {
        List<CartItem> cartItems = await _unitOfWork.CartItemRepository.GetAllAsync(p => !p.isDeleted, includes);
        if (cartItems is null)
        {
            return new ErrorDataResult<List<CartItemGetDto>>("CartItemlar Tapilmadi");
        }
        return new SuccessDataResult<List<CartItemGetDto>>(_mapper.Map<List<CartItemGetDto>>(cartItems));
    }

    public async Task<IDataResult<CartItemGetDto>> GetByIdAsync(int id, params string[] includes)
    {
        CartItem cartItem = await _unitOfWork.CartItemRepository.GetAsync(b => b.Id == id && !b.isDeleted, includes);
        if (cartItem is null)
        {
            return new ErrorDataResult<CartItemGetDto>("CartItem Tapilmadi");
        }
        return new SuccessDataResult<CartItemGetDto>(_mapper.Map<CartItemGetDto>(cartItem));
    }

    public async Task<IResult> CreateAsync(CartItemPostDto dto)
    {
        CartItem cartItem = _mapper.Map<CartItem>(dto);
        await _unitOfWork.CartItemRepository.CreateAsync(cartItem);
        int result = await _unitOfWork.SaveAsync();
        if (result is 0)
        {
            return new ErrorResult("CartItem Yaradila bilmedi");
        }
        return new SuccessResult("CartItem Yaradildi");
    }
    public async Task<IResult> UpdateAsync(CartItemUpdateDto dto)
    {
        CartItem cartItem = await _unitOfWork.CartItemRepository.GetAsync(b => b.Id == dto.Id && !b.isDeleted);
        blog = _mapper.Map<CartItem>(dto);
        _unitOfWork.CartItemRepository.Update(cartItem);
        int result = await _unitOfWork.SaveAsync();
        if (result is 0)
        {
            return new ErrorResult("CartItem Yenilene bilmedi");
        }
        return new SuccessResult("CartItem Yenilendi");
    }
    public async Task<IResult> HardDeleteByIdAsync(int id)
    {
        CartItem cartItem = await _unitOfWork.CartItemRepository.GetAsync(b => b.Id == id && !b.isDeleted);
        _unitOfWork.CartItemRepository.Delete(cartItem);
        int result = await _unitOfWork.SaveAsync();
        if (result is 0)
        {
            return new ErrorResult("CartItem Siline bilmedi");
        }
        return new SuccessResult("CartItem Silindi");
    }

    public async Task<IResult> SoftDeleteByIdAsync(int id)
    {
        CartItem cartItem = await _unitOfWork.CartItemRepository.GetAsync(b => b.Id == id && !b.isDeleted);
        cartItem.isDeleted = true;
        _unitOfWork.CartItemRepository.Update(cartItem);
        int result = await _unitOfWork.SaveAsync();
        if (result is 0)
        {
            return new ErrorResult("CartItem Siline bilmedi");
        }
        return new SuccessResult("CartItem Silindi");
    }
}

