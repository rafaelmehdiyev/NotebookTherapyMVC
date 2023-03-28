namespace DataAccessLayer.Configurations;

public class ColorConfiguration : IEntityTypeConfiguration<Color>
{
    public void Configure(EntityTypeBuilder<Color> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b => b.ColorCode).IsRequired().HasMaxLength(8);
 
        builder.Property(b => b.isDeleted).IsRequired().HasDefaultValue(false);
        builder.Property(b => b.CreatedDate).IsRequired().HasDefaultValueSql("GETUTCDATE()");

        //Relations
        builder.HasMany(b => b.Products).WithOne(b => b.Color).HasForeignKey(b => b.ColorId);
    }
}
