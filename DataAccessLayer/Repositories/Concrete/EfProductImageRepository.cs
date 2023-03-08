namespace DataAccessLayer.Repositories.Concrete;

public class EfProductImageRepository : EntityRepositoryBase<ProductImage, AppDbContext>, IProductImageRepository
{
    private readonly AppDbContext _context;

    public EfProductImageRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }
}

