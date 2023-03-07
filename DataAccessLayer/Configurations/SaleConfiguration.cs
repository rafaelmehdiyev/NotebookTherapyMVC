namespace DataAccessLayer.Configurations;

public class SaleConfiguration : IEntityTypeConfiguration<Sale>
{
    public void Configure(EntityTypeBuilder<Sale> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b => b.isDeleted).IsRequired().HasDefaultValue(false);
        builder.Property(b => b.CreatedDate).IsRequired().HasDefaultValueSql("GETUTCDATE()");

        //Relations
        builder.HasMany(b => b.SaleItems).WithOne(b => b.Sale).HasForeignKey(b => b.SaleId);
        builder.HasOne(b => b.User).WithMany(b => b.Sales).HasForeignKey(b => b.UserId);
    }
}
