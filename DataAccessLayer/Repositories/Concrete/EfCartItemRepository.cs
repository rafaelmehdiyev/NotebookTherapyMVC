namespace DataAccessLayer.Repositories.Concrete;

public class EfCartItemRepository : EntityRepositoryBase<CartItem, AppDbContext>, ICartItemRepository
{
    private readonly AppDbContext _context;

    public EfCartItemRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }
}







