namespace NotebookTherapyMVC;

public static class Includes
{
    public static readonly string[] UserIncludes = { "Cart", "Favourites.Product", "Shippings", "Sales", "Reviews","AspNetUserRoles" };
    public static readonly string[] ShippingIncludes = { "User" };
    public static readonly string[] CategoryIncludes = { "Products" };
    public static readonly string[] ProductIncludes = { "ProductImages", "Category", "ProductCollection", "ProductSizes.Size", "ProductBundles.Bundle", "Favourites.User","Color" };
    public static readonly string[] CartIncludes = { "CartItems.Product.ProductImages", "User" };
    public static readonly string[] CartItemIncludes = { "Product.ProductImages" };
    public static readonly string[] FAQCategoryIncludes = { "FAQs" };
    public static readonly string[] FAQIncludes = { "FAQCategory" };
}
