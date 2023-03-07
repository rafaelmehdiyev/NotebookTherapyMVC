namespace DataAccessLayer.Configurations;

public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
{
    public void Configure(EntityTypeBuilder<CartItem> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b => b.isDeleted).IsRequired().HasDefaultValue(false);
        builder.Property(b => b.CreatedDate).IsRequired().HasDefaultValueSql("GETUTCDATE()");

        //Relations
        builder.HasOne(b => b.Product).WithMany(b => b.CartItems).HasForeignKey(b => b.ProductId);
        builder.HasOne(b => b.Cart).WithMany(b => b.CartItems).HasForeignKey(b => b.CartId);
    }
}