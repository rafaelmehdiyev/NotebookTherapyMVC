namespace DataAccessLayer.Configurations;

public class FAQCategoryConfiguration : IEntityTypeConfiguration<FAQCategory>
{
    public void Configure(EntityTypeBuilder<FAQCategory> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b => b.Name).IsRequired().HasMaxLength(255);
        builder.Property(b => b.isDeleted).IsRequired().HasDefaultValue(false);
        builder.Property(b => b.CreatedDate).IsRequired().HasDefaultValueSql("GETUTCDATE()");

        //Relations
        builder.HasMany(b => b.FAQs).WithOne(b => b.FAQCategory).HasForeignKey(b => b.FAQCategoryId);
    }
}