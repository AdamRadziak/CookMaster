using System;
using System.Collections.Generic;

namespace CookMaster.Persistance.SqlServer.Model;

public partial class UserMenuRecipe
{
    public int? IdUser { get; set; }

    public int? IdUserMenu { get; set; }

    public int? IdRecipe { get; set; }

    public virtual Recipe? IdRecipeNavigation { get; set; }

    public virtual UserMenu? IdUserMenuNavigation { get; set; }

    public virtual User? IdUserNavigation { get; set; }
}
