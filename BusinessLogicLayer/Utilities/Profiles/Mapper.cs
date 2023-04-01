namespace BusinessLogicLayer.Utilities.Profiles;

public class Mapper : Profile
{
    public Mapper()
    {
        //Auth
        CreateMap<RegisterDto, AppUser>();
        CreateMap<AppUser, UserGetDto>().ReverseMap();

        //Role
        CreateMap<IdentityRole, RoleGetDto>();
        CreateMap<RolePostDto, IdentityRole>();
        CreateMap<RoleUpdateDto, IdentityRole>();
        CreateMap<RoleGetDto, RoleUpdateDto>();

        //Blog
        CreateMap<Blog, BlogGetDto>();
        CreateMap<BlogPostDto, Blog>();
        CreateMap<BlogUpdateDto, Blog>()
            .ForMember(x => x.CreatedDate, opt => opt.MapFrom(src => DateTime.UtcNow));
        CreateMap<BlogGetDto, BlogUpdateDto>();

        //Color
        CreateMap<Color, ColorGetDto>();
        CreateMap<ColorPostDto, Color>();
        CreateMap<ColorUpdateDto, Color>()
            .ForMember(x => x.CreatedDate, opt => opt.MapFrom(src => DateTime.UtcNow));
        CreateMap<ColorGetDto, ColorUpdateDto>();

        //Cart
        CreateMap<Cart, CartGetDto>();
        CreateMap<CartPostDto, Cart>();
        CreateMap<CartUpdateDto, Cart>()
            .ForMember(x => x.CreatedDate, opt => opt.MapFrom(src => DateTime.UtcNow));
        CreateMap<CartGetDto, CartUpdateDto>();

        //CartItem
        CreateMap<CartItem, CartItemGetDto>();
        CreateMap<CartItemPostDto, CartItem>();
        CreateMap<CartItemUpdateDto, CartItem>()
            .ForMember(x => x.CreatedDate, opt => opt.MapFrom(src => DateTime.UtcNow));
        CreateMap<CartItemGetDto, CartItemUpdateDto>();

        //Category
        CreateMap<Category, CategoryGetDto>();
        CreateMap<CategoryPostDto, Category>();
        CreateMap<CategoryUpdateDto, Category>()
            .ForMember(x => x.CreatedDate, opt => opt.MapFrom(src => DateTime.UtcNow));
        CreateMap<CategoryGetDto, CategoryUpdateDto>();

        //FAQCategory
        CreateMap<FAQCategory, FAQCategoryGetDto>();
        CreateMap<FAQCategoryPostDto, FAQCategory>();
        CreateMap<FAQCategoryUpdateDto, FAQCategory>()
            .ForMember(x => x.CreatedDate, opt => opt.MapFrom(src => DateTime.UtcNow));
        CreateMap<FAQCategoryGetDto, FAQCategoryUpdateDto>();
        //FAQ
        CreateMap<FAQ, FAQGetDto>();
        CreateMap<FAQPostDto, FAQ>();
        CreateMap<FAQUpdateDto, FAQ>()
            .ForMember(x => x.CreatedDate, opt => opt.MapFrom(src => DateTime.UtcNow));
        CreateMap<FAQGetDto, FAQUpdateDto>();

        //Favourite
        CreateMap<Favourite, FavouriteGetDto>();
        CreateMap<FavouritePostDto, Favourite>();
        CreateMap<FavouriteUpdateDto, Favourite>();
        CreateMap<FavouriteGetDto, FavouriteUpdateDto>();

        //Product
        CreateMap<Product, ProductGetDto>();
        CreateMap<ProductPostDto, Product>();
        CreateMap<ProductUpdateDto, Product>()
            .ForMember(x => x.CreatedDate, opt => opt.MapFrom(src => DateTime.UtcNow));
        CreateMap<ProductGetDto, ProductUpdateDto>();

        //Size
        CreateMap<Size, SizeGetDto>();
        CreateMap<SizePostDto, Size>();
        CreateMap<SizeUpdateDto, Size>()
            .ForMember(x => x.CreatedDate, opt => opt.MapFrom(src => DateTime.UtcNow));
        CreateMap<SizeGetDto, SizeUpdateDto>();

        //ProductSize
        CreateMap<ProductSize, ProductSizeGetDto>();
        CreateMap<ProductSizePostDto, ProductSize>();
        CreateMap<ProductSizeUpdateDto, ProductSize>();
        CreateMap<ProductSizeGetDto, ProductSizeUpdateDto>();

        //Bundle
        CreateMap<Bundle, BundleGetDto>();
        CreateMap<BundlePostDto, Bundle>();
        CreateMap<BundleUpdateDto, Bundle>()
            .ForMember(x => x.CreatedDate, opt => opt.MapFrom(src => DateTime.UtcNow));
        CreateMap<BundleGetDto, BundleUpdateDto>();

        //ProductBundle
        CreateMap<ProductBundle, ProductBundleGetDto>();
        CreateMap<ProductBundlePostDto, ProductBundle>();
        CreateMap<ProductBundleUpdateDto, ProductBundle>();
        CreateMap<ProductBundleGetDto, ProductBundleUpdateDto>();

        //ProductCollection
        CreateMap<ProductCollection, ProductCollectionGetDto>();
        CreateMap<ProductCollectionPostDto, ProductCollection>();
        CreateMap<ProductCollectionUpdateDto, ProductCollection>()
            .ForMember(x => x.CreatedDate, opt => opt.MapFrom(src => DateTime.UtcNow));
        CreateMap<ProductCollectionGetDto, ProductCollectionUpdateDto>()
            .ForMember(x => x.CollectionHeaderImage, opt => opt.Ignore())
            .ForMember(x => x.CollectionFooterImage, opt => opt.Ignore())
            .ForMember(x => x.CollectionItemBackgroundImage, opt => opt.Ignore());

        //Review
        CreateMap<Review, ReviewGetDto>();
        CreateMap<ReviewPostDto, Review>();
        CreateMap<ReviewUpdateDto, Review>()
            .ForMember(x => x.CreatedDate, opt => opt.MapFrom(src => DateTime.UtcNow));
        CreateMap<ReviewGetDto, ReviewUpdateDto>();

        //Shipping
        CreateMap<Shipping, ShippingGetDto>();
        CreateMap<ShippingPostDto, Shipping>();
        CreateMap<ShippingUpdateDto, Shipping>()
            .ForMember(x => x.CreatedDate, opt => opt.MapFrom(src => DateTime.UtcNow));
        CreateMap<ShippingGetDto, ShippingUpdateDto>();

        //Sale
        CreateMap<Sale, SaleGetDto>();
        CreateMap<SalePostDto, Sale>();
        CreateMap<SaleUpdateDto, Sale>()
            .ForMember(x => x.CreatedDate, opt => opt.MapFrom(src => DateTime.UtcNow));
        CreateMap<SaleGetDto, SaleUpdateDto>();

        //SaleItem
        CreateMap<SaleItem, SaleItemGetDto>();
        CreateMap<SaleItemPostDto, SaleItem>();
        CreateMap<SaleItemUpdateDto, SaleItem>()
            .ForMember(x => x.CreatedDate, opt => opt.MapFrom(src => DateTime.UtcNow));
        CreateMap<SaleItemGetDto, SaleItemUpdateDto>();
    }
}
