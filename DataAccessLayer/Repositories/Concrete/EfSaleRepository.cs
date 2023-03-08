namespace DataAccessLayer.Repositories.Concrete;

public class EfSaleRepository : EntityRepositoryBase<Sale, AppDbContext>, ISaleRepository
{
    private readonly AppDbContext _context;

    public EfSaleRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }
}








