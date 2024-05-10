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
}
