namespace DataAccessLayer.Repositories.Concrete;

public class EfFavouriteRepository : EntityRepositoryBase<Favourite, AppDbContext>, IFavouriteRepository
{
    private readonly AppDbContext _context;

    public EfFavouriteRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }
}










