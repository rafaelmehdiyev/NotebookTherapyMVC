using Token = BusinessLogicLayer.Utilities.Security.JWT;
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
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IBlogService, BlogManager>();
        services.AddScoped<ICartService, CartManager>();
        services.AddScoped<ICartItemService, CartItemManager>();
        services.AddScoped<ICategoryService, CategoryManager>();
        services.AddScoped<IFAQCategoryService, FAQCategoryManager>();
        services.AddScoped<IFAQService, FAQManager>();
        services.AddScoped<IFavouriteService, FavouriteManager>();
        services.AddScoped<IProductService, ProductManager>();
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
        services.AddDbContext<AppDbContext>(opt =>
        {
            opt.UseSqlServer(configuration.GetConnectionString("Default"));
        });
        services.AddIdentity<AppUser, IdentityRole>().AddDefaultTokenProviders().AddEntityFrameworkStores<AppDbContext>();
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        return services;
    }
}
