namespace allspice.Models;

public class Ingredient : BaseModel<int>
{
    public string Name { get; set; }
    public string Quantity { get; set; }
    public int RecipeId { get; set; }
}