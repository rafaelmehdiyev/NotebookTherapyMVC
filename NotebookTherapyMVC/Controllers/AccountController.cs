namespace NotebookTherapyMVC.Controllers;

public class AccountController : Controller
{
    private readonly IAccountService _accountService;
    private readonly ICartService _cartService;
    private readonly ISaleService _saleService;
    private readonly ISaleItemService _saleItemService;
    private readonly IFavouriteService _favService;
    private readonly IBraintreeService _brainTreeService;
    private readonly IProductService _productService;
    private readonly IMapper _mapper;

    public AccountController(IAccountService accountService, ICartService cartService, IFavouriteService favService, IBraintreeService brainTreeService, IProductService productService, IMapper mapperService, ISaleService saleService, ISaleItemService saleItemService)
    {
        _accountService = accountService;
        _cartService = cartService;
        _favService = favService;
        _brainTreeService = brainTreeService;
        _productService = productService;
        _mapper = mapperService;
        _saleService = saleService;
        _saleItemService = saleItemService;
    }

    #region Sale(Checkout_Middleware,Checkout,Purchase,Order Confirmed)
    public async Task<IActionResult> Charge()
    {
        Tuple<CartGetDto, SaleGetDto> saleInfo = await ReadySaleAsync();
        await ReadySaleItemsAsync(saleInfo.Item1, saleInfo.Item2);
        IBraintreeGateway gateway = _brainTreeService.GetGateway();
        string clientToken = gateway.ClientToken.Generate();
        ViewBag.ClientToken = clientToken;
        IDataResult<SaleGetDto> sale = await _saleService.GetByIdAsync(saleInfo.Item2.Id, Includes.SaleIncludes);
        return View(sale.Data);
    }

    [HttpPost]
    public async Task<IActionResult> PurchaseAsync(SaleGetDto sale)
    {
        SaleGetDto model = (await _saleService.GetByIdAsync(sale.Id, Includes.SaleIncludes)).Data;
        IDataResult<UserGetDto> userResult = await _accountService.GetUserByClaims(User, Includes.UserIncludes);
        IBraintreeGateway gateway = _brainTreeService.GetGateway();
        TransactionRequest request = new TransactionRequest
        {
            Amount = model.TotalPrice,
            PaymentMethodNonce = sale.Nonce,
            Options = new TransactionOptionsRequest
            {
                SubmitForSettlement = true
            },
            Customer = new CustomerRequest
            {
                FirstName = userResult.Data.FirstName,
                LastName = userResult.Data.LastName,
                Email = userResult.Data.Email,
                CustomerId = userResult.Data.Id
            },
            LineItems = GetTransactionItems(model.SaleItems)
        };

        Result<Transaction> result = gateway.Transaction.Sale(request);
        SaleUpdateDto saleUpdate = _mapper.Map<SaleUpdateDto>(model);
        saleUpdate.UserId = userResult.Data.Id;
        if (result.IsSuccess())
        {
            saleUpdate.SaleStatus = SaleStatus.Completed;
            await _saleService.UpdateAsync(saleUpdate);
            return RedirectToAction("OrderSuccess");
        }
        else
        {
            saleUpdate.SaleStatus = SaleStatus.Cancelled;
            await _saleService.UpdateAsync(saleUpdate);
            return RedirectToAction("OrderFailed");
        }
    }

    public IActionResult OrderSuccess()
    {       
        return View();
    }

    public IActionResult OrderFailed()
    {
        return View();
    }

    #region Private Methods
    private async Task<Tuple<CartGetDto, SaleGetDto>> ReadySaleAsync()
    {
        UserGetDto currentUser = (await _accountService.GetUserByClaims(User)).Data;
        CartGetDto cartDto = (await _cartService.GetCartByUserIdAsync(currentUser.Id, Includes.CartIncludes)).Data;
        SalePostDto postDto = _mapper.Map<SalePostDto>(cartDto);
        postDto.UserId = currentUser.Id;
        postDto.SaleStatus = SaleStatus.Pending;
        SaleGetDto saleDto = (await _saleService.CreateAsync(postDto)).Data;
        return new Tuple<CartGetDto, SaleGetDto>(cartDto, saleDto);
    }
    private async Task ReadySaleItemsAsync(CartGetDto cartDto, SaleGetDto saleDto)
    {
        List<SaleItemPostDto> saleItems = _mapper.Map<List<SaleItemPostDto>>(cartDto.CartItems.Where(x => !x.isDeleted).ToList());
        saleItems.ForEach(s => s.SaleId = saleDto.Id);

        foreach (SaleItemPostDto saleItem in saleItems)
        {
            await _saleItemService.CreateAsync(saleItem);
        }
    }

    private TransactionLineItemRequest[] GetTransactionItems(List<SaleItemGetDto> saleItems)
    {
        TransactionLineItemRequest[] lineItems = new TransactionLineItemRequest[0];
        foreach (SaleItemGetDto saleItem in saleItems)
        {
            string name = saleItem.Product.Name.Length > 34
                ? saleItem.Product.Name.Substring(0, 34)
                : saleItem.Product.Name;
            TransactionLineItemRequest lineItem = new()
            {
                Name = name,
                Quantity = saleItem.Quantity,
                UnitAmount = saleItem.Product.Price,
                TotalAmount = saleItem.TotalPrice,
                LineItemKind = TransactionLineItemKind.DEBIT
            };
            Array.Resize(ref lineItems, lineItems.Length + 1);
            lineItems[lineItems.Length - 1] = lineItem;
        }
        return lineItems;
    }
    #endregion

    #endregion


    #region Auth(Login,Register,Profile,SignOut)

    [HttpGet]
    public async Task<IActionResult> Register()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Register(RegisterDto registerDto)
    {
        IDataResult<UserGetDto> result = await _accountService.Register(registerDto);
        if (!result.Success)
        {
            return BadRequest(result);
        }
        return RedirectToAction(nameof(Login));
    }
    [HttpGet]
    public async Task<IActionResult> Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginDto loginDto)
    {
        IDataResult<UserGetDto> result = await _accountService.Login(loginDto);
        if (!result.Success) { return View(result); }
        if (result.Data.Roles.Contains("Admin"))
        {
            return RedirectToAction("Index", "Home", new { area = "Manage" });
        }
        else
        {
            return RedirectToAction("Index", "Home");
        }
    }
    [Authorize(Roles = "SuperAdmin,Admin,User")]
    public async Task<IActionResult> Signout()
    {
        await _accountService.SignOutAsync();
        return RedirectToAction(nameof(Login));
    }
    [Authorize(Roles = "SuperAdmin,Admin,User")]
    public async Task<IActionResult> Profile()
    {
        IDataResult<UserGetDto> result = await _accountService.GetUserByClaims(User, Includes.UserIncludes);
        return View(result);
    }
    #endregion

    #region Shopping Cart
    [Authorize(Roles = "SuperAdmin,Admin,User")]
    public async Task<IActionResult> ShoppingCart()
    {
        IDataResult<UserGetDto> userResult = await _accountService.GetUserByClaims(User, Includes.UserIncludes);
        IDataResult<CartGetDto> cartResult = await _cartService.GetByIdAsync(userResult.Data.Cart.Id, Includes.CartIncludes);
        return View(cartResult);
    }
    public async Task<IActionResult> LoadCartItems(int id)
    {
        IDataResult<CartGetDto> cartResult = await _cartService.GetByIdAsync(id, Includes.CartIncludes);
        return PartialView("_cartItemPartial", cartResult.Data.CartItems.Where(c => !c.isDeleted).ToList());
    }
    public async Task<IActionResult> CartItemTotalPricePartial(int id)
    {
        IDataResult<CartGetDto> cartResult = await _cartService.GetByIdAsync(id, Includes.CartIncludes);
        return PartialView("_cartItemTotalPricePartial", cartResult);
    }
    #endregion

    #region Favourites
    [Authorize(Roles = "SuperAdmin,Admin,User")]
    public async Task<IActionResult> Favourite()
    {
        IDataResult<UserGetDto> userResult = await _accountService.GetUserByClaims(User, Includes.UserIncludes);
        IDataResult<List<FavouriteGetDto>> favResult = await _favService.GetAllByUserIdAsync(userResult.Data.Id, Includes.FavouriteIncludes);
        return View(favResult);
    }
    #endregion
}
