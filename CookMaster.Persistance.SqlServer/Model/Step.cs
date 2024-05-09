namespace CookMaster.Persistance.SqlServer.Model;

public partial class Step
{
    public int Id { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();
}
