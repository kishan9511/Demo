using System;
using System.Collections.Generic;

namespace Demo.Models;

public partial class AdminLogin
{
    public int Id { get; set; }

    public string? UserName { get; set; }

    public string? Password { get; set; }

    public bool? Isdeleted { get; set; }
}
