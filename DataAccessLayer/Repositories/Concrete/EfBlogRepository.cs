namespace DataAccessLayer.Repositories.Concrete;

public class EfBlogRepository : EntityRepositoryBase<Blog, AppDbContext>, IBlogRepository
{
    private readonly AppDbContext _context;

    public EfBlogRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }
}


