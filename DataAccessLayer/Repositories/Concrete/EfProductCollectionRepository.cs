namespace DataAccessLayer.Repositories.Concrete;

public class EfProductCollectionRepository : EntityRepositoryBase<ProductCollection, AppDbContext>, IProductCollectionRepository
{
    private readonly AppDbContext _context;

    public EfProductCollectionRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }
}
