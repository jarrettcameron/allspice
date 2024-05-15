namespace allspice.Models;

public class Favorite : BaseModel<int>
{
    public int RecipeId { get; set; }
    public string AccountId { get; set; }
}