using System;
using System.Collections.Generic;

namespace Demo.Models;

public partial class Size
{
    public int Sizeid { get; set; }

    public string? SizeName { get; set; }

    public bool? Isdeleted { get; set; }

    public virtual ICollection<ProductSize> ProductSizes { get; } = new List<ProductSize>();
}
