using System;
using System.Collections.Generic;

namespace Demo.Models;

public partial class Cart
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Price { get; set; }

    public string? Image { get; set; }

    public string? Quantity { get; set; }

    public string? Total { get; set; }

    public bool? Isdeleted { get; set; }
}
