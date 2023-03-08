namespace DataAccessLayer.Repositories.Concrete;

public class EfReviewRepository : EntityRepositoryBase<Review, AppDbContext>, IReviewRepository
{
    private readonly AppDbContext _context;

    public EfReviewRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }
}
