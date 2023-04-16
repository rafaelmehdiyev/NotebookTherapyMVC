namespace BusinessLogicLayer.Utilities.Constants;
static class Messages
{
	#region Success Messages
	public static string Created(string entity)
	{
		return $"The {entity} has been created";
	}
	public static string Updated(string entity)
	{
		return $"The {entity} has been updated.";
	}
	public static string Deleted(string entity)
	{
		return $"The {entity} has been deleted.";
	}
	public static string Recovered(string entity)
	{
		return $"The {entity} has been recovered.";
	}
	#endregion

	#region Error Messages
	public static string NotFound(string entity)
	{
		return $"{entity} is not found.";
	}
	public static string NotCreated(string entity)
	{
		return $"The {entity} could not be created";
	}
	public static string NotUpdated(string entity)
	{
		return $"The {entity} could not be updated.";
	}
	public static string NotDeleted(string entity)
	{
		return $"The {entity} could not be deleted.";
	}
	public static string NotRecovered(string entity)
	{
		return $"The {entity} could not be recovered.";
	}
	public static string EnterValid(string entity)
	{
		return $" Enter Valid {entity}.";
	}
	#endregion

	#region Message Entity
	public static string Blog = "Blog";
	public static string Bundle = "Bundle";
	public static string ProductBundle = "ProductBundle";
	public static string CartItem = "CartItem";
	public static string Cart = "Cart";
	public static string Category = "Category";
	public static string Color = "Color";
	public static string FAQCategory = "FAQCategory";
	public static string FAQ = "FAQ";
	public static string Favourite = "Favourite";
	public static string Size = "Size";
	public static string ProductSize = "ProductSize";
	public static string ProductCollection = "ProductCollection";
	public static string Product = "Product";
	public static string Review = "Review";
	public static string Sale = "Sale";
	public static string SaleItem = "SaleItem";
	public static string Shipping = "Shipping";
	public static string Role = "Role";
    #endregion
}

