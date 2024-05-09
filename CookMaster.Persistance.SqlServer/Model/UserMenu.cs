namespace CookMaster.Persistance.SqlServer.Model;

public partial class UserMenu
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? RecipeCategory { get; set; }

    public int? IdMenuRecipe { get; set; }

    public virtual Recipe? IdMenuRecipeNavigation { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
