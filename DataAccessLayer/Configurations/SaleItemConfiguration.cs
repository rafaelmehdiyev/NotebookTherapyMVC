namespace DataAccessLayer.Configurations;

public class SaleItemConfiguration : IEntityTypeConfiguration<SaleItem>
{
    public void Configure(EntityTypeBuilder<SaleItem> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b => b.Quantity).IsRequired().HasMaxLength(255);
        builder.Property(b => b.isDeleted).IsRequired().HasDefaultValue(false);
        builder.Property(b => b.CreatedDate).IsRequired().HasDefaultValueSql("GETUTCDATE()");

        //Relations
        builder.HasOne(b => b.Sale).WithMany(b => b.SaleItems).HasForeignKey(b => b.SaleId);
        builder.HasOne(b => b.Product).WithMany(b => b.SaleItems).HasForeignKey(b => b.ProductId);
    }
}