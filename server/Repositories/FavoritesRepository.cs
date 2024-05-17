namespace allspice.Repositories;

public class FavoritesRepository
{
    private readonly IDbConnection _db;

    public FavoritesRepository(IDbConnection db)
    {
        _db = db;
    }

    public FavoriteView CreateFavorite(Favorite favoriteData)
    {
        string sql = @"
        INSERT INTO favorite(recipeId, accountId)
        VALUES(@RecipeId, @AccountId);
        SELECT * FROM favorite
        JOIN recipe ON recipe.id = favorite.recipeId
        JOIN accounts ON favorite.accountId = accounts.id
        WHERE favorite.id = LAST_INSERT_ID();";
        FavoriteView favorite = _db.Query<Favorite, FavoriteView, Profile, FavoriteView>(sql, (favorite, recipe, profile) =>
        {
            recipe.Creator = profile;
            recipe.FavoriteId = favorite.Id;
            recipe.CreatedAt = favorite.CreatedAt;
            recipe.UpdatedAt = favorite.UpdatedAt;
            return recipe;
        }, favoriteData).FirstOrDefault();
        return favorite;
    }

    public List<FavoriteView> GetFavoritesOnAccount(Account userInfo)
    {
        string sql = @"
        SELECT * FROM favorite
        JOIN recipe ON recipe.id = favorite.recipeId
        JOIN accounts ON favorite.accountId = accounts.id
        WHERE favorite.accountId = @Id;";
        List<FavoriteView> favorites = _db.Query<Favorite, FavoriteView, Profile, FavoriteView>(sql, (favorite, recipe, profile) =>
        {
            recipe.Creator = profile;
            recipe.FavoriteId = favorite.Id;
            recipe.CreatedAt = favorite.CreatedAt;
            recipe.UpdatedAt = favorite.UpdatedAt;
            return recipe;
        }, userInfo).ToList();
        return favorites;
    }

    public Favorite GetFavoriteById(int favoriteId)
    {
        string sql = "SELECT * FROM favorite WHERE id = @favoriteId;";
        return _db.Query<Favorite>(sql, new { favoriteId }).FirstOrDefault();
    }

    public void DestroyFavorite(int favoriteId)
    {
        string sql = "DELETE FROM favorite WHERE id = @favoriteId;";
        _db.Execute(sql, new { favoriteId });
    }
}