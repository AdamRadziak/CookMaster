using System;
using System.Collections.Generic;

namespace CookMaster.Persistance.SqlServer.Model;

public partial class Product
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Category { get; set; }

    public string? Amount { get; set; }

    public virtual ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();
}
