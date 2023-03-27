namespace DataAccessLayer.Configurations;

public class ProductBundleConfiguration : IEntityTypeConfiguration<ProductBundle>
{
    public void Configure(EntityTypeBuilder<ProductBundle> builder)
    {
        builder.HasKey(b => b.Id);
        //Relations
        builder.HasOne(b => b.Product).WithMany(b => b.ProductBundles).HasForeignKey(b => b.ProductId);
        builder.HasOne(b => b.Bundle).WithMany(b => b.ProductBundles).HasForeignKey(b => b.BundleId);
    }
}
