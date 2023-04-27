namespace DataAccessLayer.Configurations;

public class ProductCollectionConfiguration : IEntityTypeConfiguration<ProductCollection>
{
    public void Configure(EntityTypeBuilder<ProductCollection> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b => b.CollectionIcon).IsRequired().HasMaxLength(3);
        builder.Property(b => b.Name).IsRequired().HasMaxLength(255);
        builder.Property(b => b.Description).IsRequired().HasMaxLength(255);
        builder.Property(b => b.isDeleted).IsRequired().HasDefaultValue(false);
        builder.Property(b => b.CreatedDate).IsRequired().HasDefaultValueSql("GETUTCDATE()");

        //Relations
        builder.HasMany(b => b.Products).WithOne(b => b.ProductCollection).HasForeignKey(b => b.ProductCollectionId);
    }
}

