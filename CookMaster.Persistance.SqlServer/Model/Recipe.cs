using System;
using System.Collections.Generic;

namespace CookMaster.Persistance.SqlServer.Model;

public partial class Recipe
{
    public int Id { get; set; }

    public int? IdMenu { get; set; }

    public int? IdUser { get; set; }

    public string? Name { get; set; }

    public string? PrepareTime { get; set; }

    public int? MealCount { get; set; }

    public double? Rate { get; set; }

    public double? Popularity { get; set; }

    public string? Description { get; set; }

    public virtual UserMenu? IdMenuNavigation { get; set; }

    public virtual User? IdUserNavigation { get; set; }

    public virtual ICollection<Photo> Photos { get; set; } = new List<Photo>();

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    public virtual ICollection<Step> Steps { get; set; } = new List<Step>();
}
