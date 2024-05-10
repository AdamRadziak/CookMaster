using System;
using System.Collections.Generic;

namespace CookMaster.Persistance.SqlServer.Model;

public partial class Step
{
    public int Id { get; set; }

    public string? Description { get; set; }
}
