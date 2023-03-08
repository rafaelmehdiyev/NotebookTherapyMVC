namespace DataAccessLayer.Repositories.Concrete;

public class EfCartRepository : EntityRepositoryBase<Cart, AppDbContext>, ICartRepository
{
    private readonly AppDbContext _context;

    public EfCartRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }
}






