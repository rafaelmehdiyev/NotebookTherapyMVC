﻿namespace NotebookTherapyMVC;

public static class Includes
{
    public static readonly string[] UserIncludes = {
        "Cart",
        "Favourites.Product",
        "Shippings",
        "Sales",
        "Reviews.Product"
    };
    public static readonly string[] SizeIncludes = {
        "ProductSizes",
        "ProductSizes.Product.ProductImages",
        "ProductSizes.Product.ProductCollection",
        "ProductSizes.Product.ProductBundles.Bundle",
        "ProductSizes.Product.Favourites.User",
        "ProductSizes.Product.Color"
    };
    public static readonly string[] CategoryIncludes = {
        "Products",
        "Products.ProductImages",
        "Products.ProductCollection",
        "Products.ProductSizes.Size",
        "Products.ProductBundles.Bundle",
        "Products.Favourites.User",
        "Products.Color"
    };
    public static readonly string[] ProductCollectionIncludes = {
        "Products" ,
        "Products.ProductImages",
        "Products.ProductSizes.Size",
        "Products.ProductBundles.Bundle",
        "Products.Favourites.User",
        "Products.Color"
    };
    public static readonly string[] ColorIncludes = {
        "Products",
        "Products.ProductImages",
        "Products.ProductCollection",
        "Products.ProductSizes.Size",
        "Products.ProductBundles.Bundle",
        "Products.Favourites.User",
        "Products.Color"
    };
    public static readonly string[] BundleIncludes = {
        "ProductBundles",
        "ProductBundles.Product",
        "ProductBundles.Product.ProductImages",
        "ProductBundles.Product.ProductCollection",
        "ProductBundles.Product.ProductSizes.Size",
        "ProductBundles.Product.Favourites.User",
        "ProductBundles.Product.Color"
    };
    public static readonly string[] ProductIncludes = {
        "ProductImages",
        "Category",
        "ProductCollection",
        "ProductSizes.Size",
        "ProductBundles.Bundle",
        "Favourites.User",
        "Color",
        "Reviews.User",
        "SaleItems"
    };
    public static readonly string[] FavouriteIncludes = {
        "Product.ProductImages",
        "User"
    };
    public static readonly string[] CartIncludes = {
        "CartItems.Product.ProductImages",
        "User"
    };
    public static readonly string[] CartItemIncludes = {
        "Product.ProductImages"
    };
    public static readonly string[] SaleIncludes = {
        "SaleItems.Product.ProductImages",
        "User"
    };
    public static readonly string[] SaleItemIncludes = {
        "Product.ProductImages",
        "Sale.User"
    };
    public static readonly string[] ReviewIncludes = {
        "Product.ProductImages",
        "User"
    };
    public static readonly string[] FAQCategoryIncludes = {
        "FAQs"
    };
    public static readonly string[] FAQIncludes = {
        "FAQCategory"
    };
    public static readonly string[] RoleIncludes = {
        "Users"
    };
    public static readonly string[] ShippingIncludes = {
        "User"
    };
}
