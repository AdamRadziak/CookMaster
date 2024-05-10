using System;
using System.Collections.Generic;

namespace CookMaster.Persistance.SqlServer.Model;

public partial class Photo
{
    public int Id { get; set; }

    public string? FileName { get; set; }

    public byte[]? Data { get; set; }

    public string? FilePath { get; set; }
}
