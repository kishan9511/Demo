using System;
using System.Collections.Generic;

namespace Demo.Models;

public partial class ProductSerise
{
    public int Seriseid { get; set; }

    public int? Modelid { get; set; }

    public string? SeriseNumber { get; set; }

    public bool? Isdeleted { get; set; }

    public int? SubCategoryId { get; set; }

    public int? ProductCategoryId { get; set; }

    public int? BrandId { get; set; }

    public virtual ProductBrand? Brand { get; set; }

    public virtual ProductModel? Model { get; set; }

    public virtual ProductCategory? ProductCategory { get; set; }

    public virtual ICollection<ProductEntry> ProductEntries { get; } = new List<ProductEntry>();

    public virtual SubCategory? SubCategory { get; set; }
}
