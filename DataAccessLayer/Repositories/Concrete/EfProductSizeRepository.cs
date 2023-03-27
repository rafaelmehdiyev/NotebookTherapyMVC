namespace DataAccessLayer.Repositories.Concrete;

public class EfProductSizeRepository : EntityRepositoryBase<ProductSize, AppDbContext>, IProductSizeRepository
{
    private readonly AppDbContext _context;

    public EfProductSizeRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }
}
