namespace BusinessLogicLayer.Utilities.Extensions;
public static class ServiceCollectionExtension
{
    public static IServiceCollection AddBusinessConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        Token.TokenOptions tokenOptions = configuration.GetSection("TokenOptions").Get<Token.TokenOptions>();
        services.AddFluentValidationAutoValidation(opt =>
        {
            opt.DisableDataAnnotationsValidation = false;
        }).AddFluentValidationClientsideAdapters();
        services.AddControllersWithViews();
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IAccountService, AccountManager>();
        services.AddScoped<IRoleSevice, RoleManager>();
        services.AddScoped<ITokenHelper, JWTHelper>();
        services.AddScoped<IBlogService, BlogManager>();
        services.AddScoped<ICartService, CartManager>();
        services.AddScoped<IColorService, ColorManager>();
        services.AddScoped<ICartItemService, CartItemManager>();
        services.AddScoped<ICategoryService, CategoryManager>();
        services.AddScoped<IFAQCategoryService, FAQCategoryManager>();
        services.AddScoped<IFAQService, FAQManager>();
        services.AddScoped<IFavouriteService, FavouriteManager>();
        services.AddScoped<IProductService, ProductManager>();
        services.AddScoped<ISizeService, SizeManager>();
        services.AddScoped<IBundleService, BundleManager>();
        services.AddScoped<IProductSizeService, ProductSizeManager>();
        services.AddScoped<IProductBundleService, ProductBundleManager>();
        services.AddScoped<IProductCollectionService, ProductCollectionManager>();
        services.AddScoped<IReviewService, ReviewManager>();
        services.AddScoped<ISaleService, SaleManager>();
        services.AddScoped<ISaleItemService, SaleItemManager>();
        services.AddScoped<IShippingService, ShippingManager>();
        services.AddTransient<IBraintreeService, BraintreeManager>();
        //services.AddAuthentication().AddJwtBearer(opt =>
        //{
        //    opt.TokenValidationParameters = new TokenValidationParameters
        //    {
        //        ValidateIssuer = true,
        //        ValidateAudience = true,
        //        ValidateLifetime = true,
        //        ValidateIssuerSigningKey = true,
        //        ValidIssuer = tokenOptions.Issuer,
        //        ValidAudience = tokenOptions.Auidence,
        //        IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
        //    };
        //});
        services.AddAuthentication(opt =>
        {
            opt.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        });
        services.AddAuthorization();
        services.AddDbContext<AppDbContext>(opt =>
        {
            opt.UseSqlServer(configuration.GetConnectionString("DefaultR"));
        });
        services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        return services;
    }
}
