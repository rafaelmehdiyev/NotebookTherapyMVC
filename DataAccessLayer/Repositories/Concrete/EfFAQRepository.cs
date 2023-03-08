namespace DataAccessLayer.Repositories.Concrete;

public class EfFAQRepository : EntityRepositoryBase<FAQ, AppDbContext>, IFAQRepository
{
    private readonly AppDbContext _context;

    public EfFAQRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }
}



