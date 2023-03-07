namespace DataAccessLayer.Configurations;

public class FavouriteConfiguration : IEntityTypeConfiguration<Favourite>
{
    public void Configure(EntityTypeBuilder<Favourite> builder)
    {
        builder.HasKey(c => new { c.ProductId, c.UserId });

        //Relations
        builder.HasOne(c => c.Product).WithMany(c => c.Favourites).HasForeignKey(c => c.ProductId);
        builder.HasOne(c => c.User).WithMany(c => c.Favourites).HasForeignKey(c => c.UserId);
    }
}