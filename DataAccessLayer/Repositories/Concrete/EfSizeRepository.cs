namespace DataAccessLayer.Repositories.Concrete;

public class EfSizeRepository : EntityRepositoryBase<Size, AppDbContext>, ISizeRepository
{
    private readonly AppDbContext _context;

    public EfSizeRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }
}
