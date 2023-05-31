using System;
using System.Collections.Generic;

namespace Demo.Models;

public partial class Deal
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? Subtitle { get; set; }

    public string? Details { get; set; }

    public string? Percentage { get; set; }

    public string? Image { get; set; }

    public bool? Isdeleted { get; set; }
}
