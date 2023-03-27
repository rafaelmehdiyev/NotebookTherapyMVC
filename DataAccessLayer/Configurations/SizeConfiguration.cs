namespace DataAccessLayer.Configurations;

public class SizeConfiguration : IEntityTypeConfiguration<Size>
{
    public void Configure(EntityTypeBuilder<Size> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b => b.Name).IsRequired();
        builder.Property(b => b.isDeleted).IsRequired().HasDefaultValue(false);
        builder.Property(b => b.CreatedDate).IsRequired().HasDefaultValueSql("GETUTCDATE()");

        //Relations
        builder.HasMany(b => b.ProductSizes).WithOne(b => b.Size).HasForeignKey(b => b.SizeId);
    }
}
