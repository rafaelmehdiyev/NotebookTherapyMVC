namespace DataAccessLayer.Configurations;

public class CartConfiguration : IEntityTypeConfiguration<Cart>
{
    public void Configure(EntityTypeBuilder<Cart> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b => b.isDeleted).IsRequired().HasDefaultValue(false);
        builder.Property(b => b.CreatedDate).IsRequired().HasDefaultValueSql("GETUTCDATE()");

        //Relations
        builder.HasMany(b => b.CartItems).WithOne(b => b.Cart).HasForeignKey(b => b.CartId);
        builder.HasOne(b => b.User).WithOne(b => b.Cart).HasForeignKey<AppUser>(b => b.CartId);
    }
}