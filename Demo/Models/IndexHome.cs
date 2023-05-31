using System;
using System.Collections.Generic;

namespace Demo.Models;

public partial class IndexHome
{
    public int Id { get; set; }

    public string? SubTitle { get; set; }

    public string? Details { get; set; }

    public string? Logo { get; set; }

    public bool? Isdeleted { get; set; }
}
