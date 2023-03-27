﻿namespace BusinessLogicLayer.Utilities.Extensions;
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
        services.AddScoped<IAuthService, AuthManager>();
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
        services.AddAuthentication().AddJwtBearer(opt =>
        {
            opt.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = tokenOptions.Issuer,
                ValidAudience = tokenOptions.Auidence,
                IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
            };
        });
        services.AddAuthorization();
        services.AddDbContext<AppDbContext>(opt =>
        {
            opt.UseSqlServer(configuration.GetConnectionString("Default"));
        });
        services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMvc();
        return services;
    }
}
