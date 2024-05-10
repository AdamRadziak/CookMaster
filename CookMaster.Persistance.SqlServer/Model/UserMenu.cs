using System;
using System.Collections.Generic;

namespace CookMaster.Persistance.SqlServer.Model;

public partial class UserMenu
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? RecipeCategory { get; set; }
}
