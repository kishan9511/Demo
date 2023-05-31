using System;
using System.Collections.Generic;

namespace Demo.Models;

public partial class ProductImage
{
    public int ProductImageid { get; set; }

    public int? ProductEntryid { get; set; }

    public string? ImageName { get; set; }

    public bool? Isdeleted { get; set; }

    public virtual ProductEntry? ProductEntry { get; set; }
}
