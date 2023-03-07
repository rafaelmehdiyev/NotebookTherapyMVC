namespace DataAccessLayer.Configurations;

public class FAQConfiguration : IEntityTypeConfiguration<FAQ>
{
    public void Configure(EntityTypeBuilder<FAQ> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b => b.Question).IsRequired();
        builder.Property(b => b.Answer).IsRequired();
        builder.Property(b => b.isAnswered).IsRequired().HasDefaultValue(false);
        builder.Property(b => b.isHidden).IsRequired().HasDefaultValue(false);
        builder.Property(b => b.isDeleted).IsRequired().HasDefaultValue(false);
        builder.Property(b => b.CreatedDate).IsRequired().HasDefaultValueSql("GETUTCDATE()");

        //Relations
        builder.HasOne(b => b.FAQCategory).WithMany(b => b.FAQs).HasForeignKey(b => b.FAQCategoryId);
    }
}