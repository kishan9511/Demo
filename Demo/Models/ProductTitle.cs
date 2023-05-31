using System;
using System.Collections.Generic;

namespace Demo.Models;

public partial class ProductTitle
{
    public int Id { get; set; }

    public string? ProductMainTitle { get; set; }

    public string? ProductDetails { get; set; }

    public bool? Isdeleted { get; set; }
}
