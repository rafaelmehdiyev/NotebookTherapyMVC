namespace DataAccessLayer.Repositories.Concrete;

public class EfProductBundleRepository : EntityRepositoryBase<ProductBundle, AppDbContext>, IProductBundleRepository
{
    private readonly AppDbContext _context;

    public EfProductBundleRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }
}
