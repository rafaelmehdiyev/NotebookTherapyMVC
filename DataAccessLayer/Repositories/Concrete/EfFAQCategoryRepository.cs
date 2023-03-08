namespace DataAccessLayer.Repositories.Concrete;

public class EfFAQCategoryRepository : EntityRepositoryBase<FAQCategory, AppDbContext>, IFAQCategoryRepository
{
    private readonly AppDbContext _context;

    public EfFAQCategoryRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }
}



