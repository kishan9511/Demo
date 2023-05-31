using System;
using System.Collections.Generic;

namespace Demo.Models;

public partial class Contactu
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Subject { get; set; }

    public string? Message { get; set; }

    public bool? Isdeleted { get; set; }
}
