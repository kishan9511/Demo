using System;
using System.Collections.Generic;

namespace Demo.Models;

public partial class Role
{
    public int RoleId { get; set; }

    public string? RoleName { get; set; }

    public bool? Isdeleted { get; set; }

    public virtual ICollection<Registration> Registrations { get; } = new List<Registration>();
}
