using System;
using System.Collections.Generic;

namespace Demo.Models;

public partial class ProductColor
{
    public int Productcolorid { get; set; }

    public int? ProductEntryid { get; set; }

    public int? Colorid { get; set; }

    public bool? Isdeleted { get; set; }

    public virtual Color? Color { get; set; }

    public virtual ProductEntry? ProductEntry { get; set; }
}
