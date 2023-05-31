using System;
using System.Collections.Generic;

namespace Demo.Models;

public partial class AboutBanner
{
    public int Id { get; set; }

    public string? MainTitle { get; set; }

    public string? SubTitle { get; set; }

    public string? SubTitleValue { get; set; }

    public string? SubTitleDiscount { get; set; }

    public string? Url { get; set; }

    public bool? Isdeleted { get; set; }

    public string? Image { get; set; }
}
