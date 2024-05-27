using System;
using System.Collections.Generic;

namespace CookMaster.Persistance.SqlServer.Model;

public partial class Step
{
    public int Id { get; set; }

    public int? IdRecipe { get; set; }

    public int? StepNum { get; set; }

    public string? Description { get; set; }

    public virtual Recipe? IdRecipeNavigation { get; set; }
}
