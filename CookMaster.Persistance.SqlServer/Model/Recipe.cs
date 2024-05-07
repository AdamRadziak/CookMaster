using System;
using System.Collections.Generic;

namespace CookMaster.Persistance.SqlServer.Model;

public partial class Recipe
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? PrepareTime { get; set; }

    public int? MealCount { get; set; }

    public double? Rate { get; set; }

    public double? Popularity { get; set; }

    public string? Description { get; set; }

    public int? IdFavourite { get; set; }

    public int? IdStepsRecipe { get; set; }

    public int? IdProductRecipe { get; set; }

    public int? IdPhoto { get; set; }

    public virtual User? IdFavouriteNavigation { get; set; }

    public virtual Photo? IdPhotoNavigation { get; set; }

    public virtual Product? IdProductRecipeNavigation { get; set; }

    public virtual Step? IdStepsRecipeNavigation { get; set; }

    public virtual ICollection<UserMenu> UserMenus { get; set; } = new List<UserMenu>();
}
