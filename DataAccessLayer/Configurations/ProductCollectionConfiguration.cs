namespace DataAccessLayer.Configurations;

public class ProductCollectionConfiguration : IEntityTypeConfiguration<ProductCollection>
{
    public void Configure(EntityTypeBuilder<ProductCollection> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b => b.Name).IsRequired().HasMaxLength(255);
        builder.Property(b => b.CollectionHeaderImage).IsRequired();
        builder.Property(b => b.CollectionFooterImage).IsRequired();
        builder.Property(b => b.CollectionItemBackgroundImage).IsRequired();
        builder.Property(b => b.CollectionColor).IsRequired();
        builder.Property(b => b.CollectionItemColor).IsRequired();
        builder.Property(b => b.CollectionButtonColor).IsRequired();
        builder.Property(b => b.isDeleted).IsRequired().HasDefaultValue(false);
        builder.Property(b => b.CreatedDate).IsRequired().HasDefaultValueSql("GETUTCDATE()");

        //Relations
        builder.HasMany(b => b.Products).WithOne(b => b.ProductCollection).HasForeignKey(b => b.ProductCollectionId);
    }
}

