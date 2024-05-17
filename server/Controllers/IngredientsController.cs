namespace allspice.Controllers;

[ApiController]
[Route("api/ingredients")]
public class IngredientsController : ControllerBase
{
    private readonly Auth0Provider _auth0provider;
    private readonly IngredientsService _ingredientsService;

    public IngredientsController(IngredientsService ingredientsService, Auth0Provider auth0Provider)
    {
        _ingredientsService = ingredientsService;
        _auth0provider = auth0Provider;
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<Ingredient>> CreateIngredient([FromBody] Ingredient ingredientData)
    {
        try
        {
            Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
            return Ok(_ingredientsService.CreateIngredient(ingredientData, userInfo));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Authorize]
    [HttpDelete("{ingredientId}")]
    public async Task<ActionResult<string>> DestroyIngredient(int ingredientId)
    {
        try
        {
            Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
            return Ok(_ingredientsService.DestroyIngredient(ingredientId, userInfo));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}