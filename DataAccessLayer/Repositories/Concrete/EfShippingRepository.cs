namespace DataAccessLayer.Repositories.Concrete;

public class EfShippingRepository : EntityRepositoryBase<Shipping, AppDbContext>, IShippingRepository
{
    private readonly AppDbContext _context;

    public EfShippingRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }
}









