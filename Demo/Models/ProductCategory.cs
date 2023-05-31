using System;
using System.Collections.Generic;

namespace Demo.Models;

public partial class ProductCategory
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public bool? Isdeleted { get; set; }

    public virtual ICollection<ProductBrand> ProductBrands { get; } = new List<ProductBrand>();

    public virtual ICollection<ProductEntry> ProductEntries { get; } = new List<ProductEntry>();

    public virtual ICollection<ProductSerise> ProductSerises { get; } = new List<ProductSerise>();

    public virtual ICollection<SubCategory> SubCategories { get; } = new List<SubCategory>();
}
