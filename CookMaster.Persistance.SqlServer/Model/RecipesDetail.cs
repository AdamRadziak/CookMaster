using System;
using System.Collections.Generic;

namespace CookMaster.Persistance.SqlServer.Model;

public partial class RecipesDetail
{
    public int? IdRecipe { get; set; }

    public int? IdStep { get; set; }

    public int? IdProduct { get; set; }

    public int? IdPhoto { get; set; }

    public virtual Photo? IdPhotoNavigation { get; set; }

    public virtual Product? IdProductNavigation { get; set; }

    public virtual Recipe? IdRecipeNavigation { get; set; }

    public virtual Step? IdStepNavigation { get; set; }
}
