using System;
using System.Collections.Generic;

namespace CookMaster.Persistance.SqlServer.Model;

public partial class Product
{
    public int Id { get; set; }

    public int? IdRecipe { get; set; }

    public string? Name { get; set; }

    public string? Category { get; set; }

    public double? Amount { get; set; }

    public string? Unit { get; set; }

    public virtual Recipe? IdRecipeNavigation { get; set; }
}
