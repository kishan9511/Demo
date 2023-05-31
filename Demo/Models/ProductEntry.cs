using System;
using System.Collections.Generic;

namespace Demo.Models;

public partial class ProductEntry
{
    public int ProductEntryid { get; set; }

    public int? Id { get; set; }

    public int? BrandId { get; set; }

    public int? Modelid { get; set; }

    public int? Seriseid { get; set; }

    public string? ProductName { get; set; }

    public decimal? ActualPrice { get; set; }

    public decimal? CurrentPrice { get; set; }

    public string? Discount { get; set; }

    public string? Descriptions { get; set; }

    public bool? IsAvailable { get; set; }

    public bool? Isdeleted { get; set; }

    public int? SubCategoryId { get; set; }

    public virtual ProductBrand? Brand { get; set; }

    public virtual ProductCategory? IdNavigation { get; set; }

    public virtual ProductModel? Model { get; set; }

    public virtual ICollection<ProductColor> ProductColors { get; } = new List<ProductColor>();

    public virtual ICollection<ProductImage> ProductImages { get; } = new List<ProductImage>();

    public virtual ICollection<ProductSize> ProductSizes { get; } = new List<ProductSize>();

    public virtual ProductSerise? Serise { get; set; }

    public virtual SubCategory? SubCategory { get; set; }
}
