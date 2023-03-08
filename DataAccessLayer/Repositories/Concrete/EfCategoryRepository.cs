namespace DataAccessLayer.Repositories.Concrete;

public class EfCategoryRepository : EntityRepositoryBase<Category,AppDbContext> , ICategoryRepository
{
    private readonly AppDbContext _context;

    public EfCategoryRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }
}