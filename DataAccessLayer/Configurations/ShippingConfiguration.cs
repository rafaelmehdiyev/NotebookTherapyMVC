namespace DataAccessLayer.Configurations;

public class ShippingConfiguration : IEntityTypeConfiguration<Shipping>
{
    public void Configure(EntityTypeBuilder<Shipping> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b => b.Country).IsRequired();
        builder.Property(b => b.Address).IsRequired();
        builder.Property(b => b.Apartment).IsRequired();
        builder.Property(b => b.City).IsRequired();
        builder.Property(b => b.isDeleted).IsRequired().HasDefaultValue(false);
        builder.Property(b => b.CreatedDate).IsRequired().HasDefaultValueSql("GETUTCDATE()");

        //Relations
        builder.HasOne(b => b.User).WithMany(b => b.Shippings).HasForeignKey(b => b.UserId);
    }
}
