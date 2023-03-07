namespace DataAccessLayer;

public class AppDbContext : IdentityDbContext<AppUser>
{
	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}

	public DbSet<Category> Categories { get; set; }
	public DbSet<ProductCollection> ProductCollections { get; set; }
	public DbSet<Product> Products { get; set; }
	public DbSet<Review> Reviews { get; set; }
	public DbSet<ProductImage> ProductImages { get; set; }
	public DbSet<Blog> Blogs { get; set; }
	public DbSet<FAQ> FAQs { get; set; }
	public DbSet<FAQCategory> FAQCategories { get; set; }
	public DbSet<Cart> Carts { get; set; }
	public DbSet<CartItem> CartItems { get; set; }
	public DbSet<Sale> Sales { get; set; }
	public DbSet<SaleItem> SaleItems { get; set; }
	public DbSet<Shipping> Shippings { get; set; }
	public DbSet<Favourite> Favourites { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
		builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }
}
