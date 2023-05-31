using System;
using System.Collections.Generic;

namespace Demo.Models;

public partial class ProductBrand
{
    public int BrandId { get; set; }

    public string? BrandName { get; set; }

    public bool? Isdeleted { get; set; }

    public int? ProductCategoryId { get; set; }

    public int? SubCategoryId { get; set; }

    public virtual ProductCategory? ProductCategory { get; set; }

    public virtual ICollection<ProductEntry> ProductEntries { get; } = new List<ProductEntry>();

    public virtual ICollection<ProductModel> ProductModels { get; } = new List<ProductModel>();

    public virtual ICollection<ProductSerise> ProductSerises { get; } = new List<ProductSerise>();

    public virtual SubCategory? SubCategory { get; set; }
}
