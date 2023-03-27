namespace DataAccessLayer.Repositories.Concrete;

public class EfColorRepository : EntityRepositoryBase<Color, AppDbContext>, IColorRepository
{
    private readonly AppDbContext _context;

    public EfColorRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }
}








