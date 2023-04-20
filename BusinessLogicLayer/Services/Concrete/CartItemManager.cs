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

    #region Get Requests
    public async Task<IDataResult<List<CartItemGetDto>>> GetAllAsync(params string[] includes)
    {
        List<CartItem> cartItems = await _unitOfWork.CartItemRepository.GetAllAsync(p => !p.isDeleted, includes);
        if (cartItems is null)
        {
            return new ErrorDataResult<List<CartItemGetDto>>(Messages.NotFound(Messages.CartItem));
        }
        return new SuccessDataResult<List<CartItemGetDto>>(_mapper.Map<List<CartItemGetDto>>(cartItems));
    }

    public async Task<IDataResult<CartItemGetDto>> GetByIdAsync(int id, params string[] includes)
    {
        CartItem cartItem = await _unitOfWork.CartItemRepository.GetAsync(b => b.Id == id && !b.isDeleted, includes);
        if (cartItem is null)
        {
            return new ErrorDataResult<CartItemGetDto>(Messages.NotFound(Messages.CartItem));
        }
        return new SuccessDataResult<CartItemGetDto>(_mapper.Map<CartItemGetDto>(cartItem));
    }

    public async Task<IDataResult<List<CartItemGetDto>>> GetAllByCartIdAsync(int id, params string[] includes)
    {
        List<CartItem> cartItems = await _unitOfWork.CartItemRepository.GetAllAsync(b => b.CartId == id && !b.isDeleted, includes);
        if (cartItems is null)
        {
            return new ErrorDataResult<List<CartItemGetDto>>(Messages.NotFound(Messages.CartItem));
        }
        return new SuccessDataResult<List<CartItemGetDto>>(_mapper.Map<List<CartItemGetDto>>(cartItems));
    }

    #endregion

    #region Post Request
    public async Task<IDataResult<CartItemGetDto>> CreateAsync(ProductGetDto dto, UserGetDto user)
    {
        CartItem cartItem = await _unitOfWork.CartItemRepository.GetAsync(c=>c.ProductId== dto.Id && c.Cart.UserId == user.Id && !c.isDeleted);
        if (cartItem is null)
        {
            cartItem = new()
            {
                ProductId = dto.Id,
                CartId = user.Cart.Id,
                TotalPrice = dto.Price
            };
            await _unitOfWork.CartItemRepository.CreateAsync(cartItem);
        }
        else
        {
            cartItem.Quantity++;
            cartItem.TotalPrice += dto.Price;
            _unitOfWork.CartItemRepository.Update(cartItem);
        }
        int result = await _unitOfWork.SaveAsync();
        if (result is 0)
        {
            return new ErrorDataResult<CartItemGetDto>(_mapper.Map<CartItemGetDto>(cartItem),Messages.NotCreated(Messages.CartItem));
        }
        return new SuccessDataResult<CartItemGetDto>(_mapper.Map<CartItemGetDto>(cartItem),"Product added to cart");
    }

    #endregion

    #region Update Requests
    public async Task<IDataResult<CartItemGetDto>> RemoveItemFromCartAsync(ProductGetDto dto, UserGetDto user,bool deleteAll = false)
    {
        CartItem cartItem = await _unitOfWork.CartItemRepository.GetAsync(c => c.ProductId == dto.Id && c.Cart.UserId == user.Id && !c.isDeleted);
        if(cartItem is null) return new ErrorDataResult<CartItemGetDto>(message: Messages.NotFound(Messages.CartItem));
        if (deleteAll)
        {
            cartItem.isDeleted = true;
            cartItem.Quantity = 0;
            cartItem.TotalPrice = 0;
        }
        else
        {
            cartItem.isDeleted = cartItem.Quantity == 1 ? true : false;
            cartItem.Quantity--;
            cartItem.TotalPrice-=dto.Price;
        }
        _unitOfWork.CartItemRepository.Update(cartItem);
        int result = await _unitOfWork.SaveAsync();
        if (result is 0)
        {
            return new ErrorDataResult<CartItemGetDto>(_mapper.Map<CartItemGetDto>(cartItem), Messages.NotUpdated(Messages.CartItem));
        }
        return new SuccessDataResult<CartItemGetDto>(_mapper.Map<CartItemGetDto>(cartItem), "Product Removed from Cart");
    }

    #endregion

    #region Delete Requests
    public async Task<IResult> HardDeleteByIdAsync(int id)
    {
        CartItem cartItem = await _unitOfWork.CartItemRepository.GetAsync(b => b.Id == id && !b.isDeleted);
        _unitOfWork.CartItemRepository.Delete(cartItem);
        int result = await _unitOfWork.SaveAsync();
        if (result is 0)
        {
            return new ErrorResult(Messages.NotDeleted(Messages.CartItem));
        }
        return new SuccessResult(Messages.Deleted(Messages.CartItem));
    }

    public async Task<IResult> SoftDeleteByIdAsync(int id)
    {
        CartItem cartItem = await _unitOfWork.CartItemRepository.GetAsync(b => b.Id == id && !b.isDeleted);
        cartItem.isDeleted = true;
        _unitOfWork.CartItemRepository.Update(cartItem);
        int result = await _unitOfWork.SaveAsync();
        if (result is 0)
        {
            return new ErrorResult(Messages.NotDeleted(Messages.CartItem));
        }
        return new SuccessResult(Messages.Deleted(Messages.CartItem));
    }

    #endregion

}

