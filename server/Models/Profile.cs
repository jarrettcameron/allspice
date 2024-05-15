namespace allspice.Models;

public class Profile : BaseModel<string>
{
    public string Name { get; set; }
    public string Picture { get; set; }
}
