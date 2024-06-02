using System;
using System.Collections.Generic;

namespace CookMaster.Persistance.SqlServer.Model;

public partial class UserMenu
{
    public int Id { get; set; }

    public int? IdUser { get; set; }

    public string? Name { get; set; }

    public string? Category { get; set; }

    public virtual User? IdUserNavigation { get; set; }

    public virtual ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();
}
