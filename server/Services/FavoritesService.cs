namespace allspice.Services;

public class FavoritesService
{
    private readonly FavoritesRepository _repository;

    public FavoritesService(FavoritesRepository repository)
    {
        _repository = repository;
    }

    public FavoriteView CreateFavorite(Favorite favoriteData, Account userInfo)
    {
        favoriteData.AccountId = userInfo.Id;
        return _repository.CreateFavorite(favoriteData);
    }

    public List<FavoriteView> GetFavoritesOnAccount(Account userInfo)
    {
        return _repository.GetFavoritesOnAccount(userInfo);
    }

    public Favorite GetFavoriteById(int favoriteId)
    {
        Favorite favorite = _repository.GetFavoriteById(favoriteId);
        if (favorite == null)
        {
            throw new Exception("Invalid id.");
        }
        return favorite;
    }

    public string DestroyFavorite(int favoriteId, Account userInfo)
    {
        Favorite favorite = GetFavoriteById(favoriteId);
        if (favorite.AccountId != userInfo.Id)
        {
            throw new Exception("Forbidden: You cannot delete something that was not created by you.");
        }
        _repository.DestroyFavorite(favoriteId);
        return "Deleted favorite.";
    }
}