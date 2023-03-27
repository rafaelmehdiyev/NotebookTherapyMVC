namespace DataAccessLayer.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b => b.Name).IsRequired().HasMaxLength(255);
        builder.Property(b => b.Description).IsRequired();
        builder.Property(b => b.DiscountPercent).HasDefaultValue(0);
        builder.Property(b => b.isSale).HasDefaultValue(false);
        builder.Property(b => b.isDeleted).IsRequired().HasDefaultValue(false);
        builder.Property(b => b.CreatedDate).IsRequired().HasDefaultValueSql("GETUTCDATE()");

        //Relations
        builder.HasOne(b => b.Category).WithMany(b => b.Products).HasForeignKey(b => b.CategoryId);
        builder.HasOne(b => b.Color).WithMany(b => b.Products).HasForeignKey(b => b.ColorId);
        builder.HasOne(b => b.ProductCollection).WithMany(b => b.Products).HasForeignKey(b => b.ProductCollectionId);
        builder.HasMany(b => b.ProductImages).WithOne(b => b.Product).HasForeignKey(b => b.ProductId);
        builder.HasMany(b => b.CartItems).WithOne(b => b.Product).HasForeignKey(b => b.ProductId);
        builder.HasMany(b => b.SaleItems).WithOne(b => b.Product).HasForeignKey(b => b.ProductId);
        builder.HasMany(b => b.Reviews).WithOne(b => b.Product).HasForeignKey(b => b.ProductId);
        builder.HasMany(b => b.Favourites).WithOne(b => b.Product).HasForeignKey(b => b.ProductId);
        builder.HasMany(b => b.ProductSizes).WithOne(b => b.Product).HasForeignKey(b => b.ProductId);
        builder.HasMany(b => b.ProductBundles).WithOne(b => b.Product).HasForeignKey(b => b.ProductId);
    }
}