namespace BusinessLogicLayer.Utilities.Profiles
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            //Blog
            CreateMap<Blog, BlogGetDto>();
            CreateMap<BlogPostDto, Blog>();
            CreateMap<BlogUpdateDto, Blog>();

            //Cart
            CreateMap<Cart, CartGetDto>();
            CreateMap<CartPostDto, Cart>();
            CreateMap<CartUpdateDto, Cart>();

            //CartItem
            CreateMap<CartItem, CartItemGetDto>();
            CreateMap<CartItemPostDto, CartItem>();
            CreateMap<CartItemUpdateDto, CartItem>();

            //Category
            CreateMap<Category, CategoryGetDto>();
            CreateMap<CategoryPostDto, Category>();
            CreateMap<CategoryUpdateDto, Category>();

            //FAQCategory
            CreateMap<FAQCategory, FAQCategoryGetDto>();
            CreateMap<FAQCategoryPostDto, FAQCategory>();
            CreateMap<FAQCategoryUpdateDto, FAQCategory>();

            //FAQ
            CreateMap<FAQ, FAQGetDto>();
            CreateMap<FAQPostDto, FAQ>();
            CreateMap<FAQUpdateDto, FAQ>();

            //Favourite
            CreateMap<Favourite, FavouriteGetDto>();
            CreateMap<FavouritePostDto, Favourite>();
            CreateMap<FavouriteUpdateDto, Favourite>();

            //Product
            CreateMap<Product,ProductGetDto>();
            CreateMap<ProductPostDto, Product>();
            CreateMap<ProductUpdateDto, Product>();

            //ProductCollection
            CreateMap<ProductCollection, ProductCollectionGetDto>();
            CreateMap<ProductCollectionPostDto, ProductCollection>();
            CreateMap<ProductCollectionUpdateDto, ProductCollection>();

            //Review
            CreateMap<Review, ReviewGetDto>();
            CreateMap<ReviewPostDto, Review>();
            CreateMap<ReviewUpdateDto, Review>();

            //Shipping
            CreateMap<Shipping, ShippingGetDto>();
            CreateMap<ShippingPostDto, Shipping>();
            CreateMap<ShippingUpdateDto, Shipping>();

            //Sale
            CreateMap<Sale, SaleGetDto>();
            CreateMap<SalePostDto, Sale>();
            CreateMap<SaleUpdateDto, Sale>();

            //SaleItem
            CreateMap<SaleItem, SaleItemGetDto>();
            CreateMap<SaleItemPostDto, SaleItem>();
            CreateMap<SaleItemUpdateDto, SaleItem>();
        }
    }
}
