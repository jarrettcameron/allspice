namespace allspice.Controllers;

[ApiController]
[Route("api/favorites")]
public class FavoritesController : ControllerBase
{
    private readonly FavoritesService _favoritesService;
    private readonly Auth0Provider _auth0provider;

    public FavoritesController(FavoritesService favoritesService, Auth0Provider auth0provider)
    {
        _favoritesService = favoritesService;
        _auth0provider = auth0provider;
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<FavoriteView>> CreateFavorite([FromBody] Favorite favoriteData)
    {
        try
        {
            Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
            return Ok(_favoritesService.CreateFavorite(favoriteData, userInfo));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Authorize]
    [HttpDelete("{favoriteId}")]
    public async Task<ActionResult<string>> DestroyFavorite(int favoriteId)
    {
        try
        {
            Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
            return Ok(_favoritesService.DestroyFavorite(favoriteId, userInfo));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}