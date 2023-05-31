using System;
using System.Collections.Generic;

namespace Demo.Models;

public partial class About
{
    public int Id { get; set; }

    public string? ShopAddress { get; set; }

    public bool? Isdeleted { get; set; }

    public string? Mobile { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public string? Fax { get; set; }

    public string? AboutTitle { get; set; }

    public string? ServiceTitle { get; set; }

    public string? WeekdayHr { get; set; }

    public string? WeekendHr { get; set; }

    public string? ServiceImage { get; set; }
}
