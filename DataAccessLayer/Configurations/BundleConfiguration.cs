namespace DataAccessLayer.Configurations;

public class BundleConfiguration : IEntityTypeConfiguration<Bundle>
{
    public void Configure(EntityTypeBuilder<Bundle> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b=>b.Name).IsRequired();
        builder.Property(b => b.isDeleted).IsRequired().HasDefaultValue(false);
        builder.Property(b => b.CreatedDate).IsRequired().HasDefaultValueSql("GETUTCDATE()");

        //Relations
        builder.HasMany(b => b.ProductBundles).WithOne(b => b.Bundle).HasForeignKey(b => b.BundleId);
    }
}
