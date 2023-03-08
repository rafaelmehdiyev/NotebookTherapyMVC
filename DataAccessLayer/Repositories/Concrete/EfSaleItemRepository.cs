namespace DataAccessLayer.Repositories.Concrete;

public class EfSaleItemRepository : EntityRepositoryBase<SaleItem, AppDbContext>, ISaleItemRepository
{
    private readonly AppDbContext _context;

    public EfSaleItemRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }
}








