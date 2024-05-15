namespace allspice.Services;

public class RecipesService
{
    private readonly RecipesRepository _repository;

    public RecipesService(RecipesRepository repository)
    {
        _repository = repository;
    }

    public Recipe CreateRecipe(Recipe recipeData, Account userInfo)
    {
        recipeData.CreatorId = userInfo.Id;
        return _repository.CreateRecipe(recipeData);
    }

    public List<Recipe> GetAllRecipes()
    {
        return _repository.GetAllRecipes();
    }

    public Recipe GetRecipeById(int recipeId)
    {
        Recipe recipe = _repository.GetRecipeById(recipeId);

        if (recipe == null)
        {
            throw new Exception("Invalid ID.");
        }
        return recipe;
    }

    public Recipe UpdateRecipe(int recipeId, Recipe recipeData, Account userInfo)
    {
        Recipe recipe = GetRecipeById(recipeId);

        if (recipe.CreatorId != userInfo.Id)
        {
            throw new Exception("Forbidden.");
        }

        recipe.Title = recipeData.Title ?? recipe.Title;
        recipe.Instructions = recipeData.Instructions ?? recipe.Instructions;

        return _repository.UpdateRecipe(recipe);
    }

    public string DestroyRecipe(int recipeId, Account userInfo)
    {
        Recipe recipe = GetRecipeById(recipeId);
        if (recipe.CreatorId != userInfo.Id)
        {
            throw new Exception("Forbidden.");
        }
        _repository.DestroyRecipe(recipeId);
        return "Deleted recipe.";
    }
}