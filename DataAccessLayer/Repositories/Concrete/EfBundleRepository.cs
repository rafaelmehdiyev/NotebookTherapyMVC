namespace DataAccessLayer.Repositories.Concrete;

public class EfBundleRepository : EntityRepositoryBase<Bundle, AppDbContext>, IBundleRepository
{
    private readonly AppDbContext _context;

    public EfBundleRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }
}
