namespace BusinessLogicLayer.Services.Abstract;
public interface IFavouriteService
{
    #region Get Requests
    Task<IDataResult<List<FavouriteGetDto>>> GetAllAsync(params string[] includes);
    Task<IDataResult<List<FavouriteGetDto>>> GetAllByUserIdAsync(string id,params string[] includes);
    Task<IDataResult<FavouriteGetDto>> GetByIdAsync(int id, params string[] includes);
    #endregion

    #region Create Requests
    Task<IDataResult<FavouriteGetDto>> CreateAsync(FavouritePostDto dto);

    #endregion

    #region Update Requests
    Task<IResult> UpdateAsync(FavouriteUpdateDto dto);

    #endregion

    #region Delete Requests
    Task<IResult> HardDeleteByIdAsync(int id);
    #endregion

    #region Other Requests
    
    #endregion
}
