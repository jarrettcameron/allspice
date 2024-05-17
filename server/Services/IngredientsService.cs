namespace allspice.Services;

public class IngredientsService
{
    private readonly IngredientsRepository _repository;
    private readonly RecipesService _recipesService;

    public IngredientsService(IngredientsRepository repository, RecipesService recipesService)
    {
        _repository = repository;
        _recipesService = recipesService;
    }

    public Ingredient CreateIngredient(Ingredient ingredientData, Account userInfo)
    {
        Recipe recipe = _recipesService.GetRecipeById(ingredientData.RecipeId);
        if (userInfo.Id != recipe.CreatorId)
        {
            throw new Exception("Cannot add ingredients to a recipe that you did not create.");
        }
        return _repository.CreateIngredient(ingredientData);
    }

    public List<Ingredient> GetIngredientsByRecipeId(int recipeId)
    {
        return _repository.GetIngredientsByRecipeId(recipeId);
    }

    public Ingredient GetIngredientById(int ingredientId)
    {
        Ingredient ingredient = _repository.GetIngredientById(ingredientId);
        if (ingredient == null)
        {
            throw new Exception("Invalid id.");
        }
        return ingredient;
    }

    public string DestroyIngredient(int ingredientId, Account userInfo)
    {
        Ingredient ingredient = GetIngredientById(ingredientId);
        Recipe recipe = _recipesService.GetRecipeById(ingredient.RecipeId);
        if (recipe.CreatorId != userInfo.Id)
        {
            throw new Exception("You cannot delete an ingredient on a recipe that you did not create.");
        }
        _repository.DestroyIngredient(ingredientId);
        return "Deleted ingredient.";
    }
}