using System;
using System.Collections.Generic;

namespace Demo.Models;

public partial class ProductSize
{
    public int ProductSizeid { get; set; }

    public int? ProductEntryid { get; set; }

    public int? Sizeid { get; set; }

    public bool? Isdeleted { get; set; }

    public virtual ProductEntry? ProductEntry { get; set; }

    public virtual Size? Size { get; set; }
}
