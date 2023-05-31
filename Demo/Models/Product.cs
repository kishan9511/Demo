using System;
using System.Collections.Generic;

namespace Demo.Models;

public partial class Product
{
    public int Id { get; set; }

    public string? ProdecuName { get; set; }

    public string? ProductPrice { get; set; }

    public string? ProductDetails { get; set; }

    public string? Image { get; set; }

    public string? ProductCart { get; set; }

    public bool? Isdeleted { get; set; }
}
