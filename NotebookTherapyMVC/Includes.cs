namespace NotebookTherapyMVC;

public static class Includes
{
    public static readonly string[] UserIncludes = { "Cart", "Favourites.Product", "Shippings", "Sales", "Reviews" };
    public static readonly string[] ShippingIncludes = { "User" };
    public static readonly string[] CategoryIncludes = { "Products", "Products.ProductImages", "Products.ProductCollection", "Products.ProductSizes.Size", "Products.ProductBundles.Bundle", "Products.Favourites.User", "Products.Color" };
    public static readonly string[] ProductCollectionIncludes = { "Products" , "Products.ProductImages", "Products.ProductSizes.Size", "Products.ProductBundles.Bundle", "Products.Favourites.User", "Products.Color"};
    public static readonly string[] ColorIncludes = { "Products", "Products.ProductImages", "Products.ProductCollection", "Products.ProductSizes.Size", "Products.ProductBundles.Bundle", "Products.Favourites.User", "Products.Color" };
    public static readonly string[] BundleIncludes = { "ProductBundles","ProductBundles.Product", "ProductBundles.Product.ProductImages", "ProductBundles.Product.ProductCollection", "ProductBundles.Product.ProductSizes.Size", "ProductBundles.Product.Favourites.User", "ProductBundles.Product.Color" };
    public static readonly string[] ProductIncludes = { "ProductImages", "Category", "ProductCollection", "ProductSizes.Size", "ProductBundles.Bundle", "Favourites.User","Color" };
    public static readonly string[] CartIncludes = { "CartItems.Product.ProductImages", "User" };
    public static readonly string[] CartItemIncludes = { "Product.ProductImages" };
    public static readonly string[] FAQCategoryIncludes = { "FAQs" };
    public static readonly string[] FAQIncludes = { "FAQCategory" };
}
