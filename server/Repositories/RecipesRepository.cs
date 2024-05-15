namespace allspice.Repositories;

public class RecipesRepository
{
    private readonly IDbConnection _db;

    public RecipesRepository(IDbConnection db)
    {
        _db = db;
    }

    public Recipe CreateRecipe(Recipe recipeData)
    {
        string sql = @"
        INSERT INTO recipe(title, instructions, img, category, creatorId) VALUES
        (@Title, @Instructions, @Img, @Category, @CreatorId);

        SELECT * FROM recipe JOIN accounts ON accounts.id = recipe.creatorId WHERE recipe.id = LAST_INSERT_ID();";

        Recipe recipe = _db.Query<Recipe, Profile, Recipe>(sql, (recipe, profile) =>
        {
            recipe.Creator = profile;
            return recipe;
        }, recipeData).FirstOrDefault();

        return recipe;
    }

    public List<Recipe> GetAllRecipes()
    {
        string sql = "SELECT * FROM recipe JOIN accounts ON accounts.id = recipe.creatorId;";

        List<Recipe> recipes = _db.Query<Recipe, Profile, Recipe>(sql, (recipe, profile) =>
        {
            recipe.Creator = profile;
            return recipe;
        }).ToList();

        return recipes;
    }

    public Recipe GetRecipeById(int recipeId)
    {
        string sql = "SELECT * FROM recipe JOIN accounts ON accounts.id = recipe.creatorId WHERE recipe.id = @recipeId;";
        Recipe recipe = _db.Query<Recipe, Profile, Recipe>(sql, (recipe, profile) =>
        {
            recipe.Creator = profile;
            return recipe;
        }, new { recipeId }).FirstOrDefault();
        return recipe;
    }

    public Recipe UpdateRecipe(Recipe recipeData)
    {
        string sql = @"
        UPDATE recipe SET
        title = @Title,
        instructions = @Instructions WHERE id = @Id;

        SELECT * FROM recipe JOIN accounts ON accounts.id = recipe.creatorId WHERE recipe.id = @Id;";

        Recipe recipe = _db.Query<Recipe, Profile, Recipe>(sql, (recipe, profile) =>
        {
            recipe.Creator = profile;
            return recipe;
        }, recipeData).FirstOrDefault();

        return recipe;
    }

    public void DestroyRecipe(int recipeId)
    {
        string sql = "DELETE FROM recipe WHERE id = @recipeId;";
        _db.Execute(sql, new { recipeId });
    }
}