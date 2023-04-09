namespace DataAccessLayer.Configurations;

public class BlogConfiguration : IEntityTypeConfiguration<Blog>
{
    public void Configure(EntityTypeBuilder<Blog> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b => b.ImagePath).IsRequired();
        builder.Property(b => b.Title).IsRequired();
        builder.Property(b => b.ShortDescription).IsRequired();
        builder.Property(b => b.Content).IsRequired();
        builder.Property(b => b.ViewCount).HasDefaultValue(0);
        builder.Property(b => b.isDeleted).IsRequired().HasDefaultValue(false);
        builder.Property(b => b.CreatedDate).IsRequired().HasDefaultValueSql("GETUTCDATE()");
    }
}