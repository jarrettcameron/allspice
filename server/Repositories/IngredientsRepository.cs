
namespace allspice.Repositories;

public class IngredientsRepository
{
    private readonly IDbConnection _db;

    public IngredientsRepository(IDbConnection db)
    {
        _db = db;
    }

    public Ingredient CreateIngredient(Ingredient ingredientData)
    {
        string sql = @"
        INSERT INTO ingredient(name, quantity, recipeId)
        VALUES (@Name, @Quantity, @RecipeId);

        SELECT * FROM ingredient WHERE id = LAST_INSERT_ID();
        ";

        Ingredient ingredient = _db.Query<Ingredient>(sql, ingredientData).FirstOrDefault();

        return ingredient;
    }

    public List<Ingredient> GetIngredientsByRecipeId(int recipeId)
    {
        string sql = @"SELECT * FROM ingredient WHERE recipeId = @recipeId;";

        List<Ingredient> ingredients = _db.Query<Ingredient>(sql, new { recipeId }).ToList();

        return ingredients;
    }

    public void DestroyIngredient(int ingredientId)
    {
        string sql = "DELETE FROM ingredient WHERE id = @ingredientId;";
        _db.Execute(sql, new { ingredientId });
    }

    public Ingredient GetIngredientById(int ingredientId)
    {
        string sql = "SELECT * FROM ingredient WHERE id = @ingredientId;";

        return _db.Query<Ingredient>(sql, new { ingredientId }).FirstOrDefault();
    }
}