namespace DataAccessLayer.Configurations;

public class ProductSizeConfiguration : IEntityTypeConfiguration<ProductSize>
{
    public void Configure(EntityTypeBuilder<ProductSize> builder)
    {
        builder.HasKey(b => b.Id);
        //Relations
        builder.HasOne(b => b.Product).WithMany(b => b.ProductSizes).HasForeignKey(b => b.ProductId);
        builder.HasOne(b => b.Size).WithMany(b => b.ProductSizes).HasForeignKey(b => b.SizeId);
    }
}