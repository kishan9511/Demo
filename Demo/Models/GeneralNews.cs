using System;
using System.Collections.Generic;

namespace Demo.Models;

public partial class GeneralNews
{
    public int Id { get; set; }

    public string? HealLine { get; set; }

    public string? Author { get; set; }

    public string? Date { get; set; }

    public string? Logo { get; set; }

    public bool? Isdeleted { get; set; }

    public string? Details { get; set; }
}
