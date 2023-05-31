using System;
using System.Collections.Generic;

namespace Demo.Models;

public partial class Color
{
    public int Colorid { get; set; }

    public string? ColorName { get; set; }

    public bool? Isdeleted { get; set; }

    public virtual ICollection<ProductColor> ProductColors { get; } = new List<ProductColor>();
}
