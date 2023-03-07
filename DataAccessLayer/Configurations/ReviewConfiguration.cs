namespace DataAccessLayer.Configurations;

public class ReviewConfiguration : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b => b.Comment).IsRequired();
        builder.Property(b => b.Rating).IsRequired();
        builder.Property(b => b.isDeleted).IsRequired().HasDefaultValue(false);
        builder.Property(b => b.CreatedDate).IsRequired().HasDefaultValueSql("GETUTCDATE()");

        //Relations
        builder.HasOne(b => b.User).WithMany(b => b.Reviews).HasForeignKey(b => b.UserId);
        builder.HasOne(b => b.Product).WithMany(b => b.Reviews).HasForeignKey(b => b.ProductId);
    }
}
