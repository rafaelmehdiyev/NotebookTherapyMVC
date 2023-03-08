namespace DataAccessLayer.Repositories.Concrete;

public class EfProductRepository : EntityRepositoryBase<Product, AppDbContext>, IProductRepository
{
    private readonly AppDbContext _context;

    public EfProductRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }
}
