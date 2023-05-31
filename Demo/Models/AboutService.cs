using System;
using System.Collections.Generic;

namespace Demo.Models;

public partial class AboutService
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? Details { get; set; }

    public bool? Isdeleted { get; set; }

    public string? Logo { get; set; }
}
