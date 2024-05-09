namespace CookMaster.Persistance.SqlServer.Model;

public partial class User
{
    public int Id { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public int? IdMenu { get; set; }

    public virtual UserMenu? IdMenuNavigation { get; set; }

    public virtual ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();
}
