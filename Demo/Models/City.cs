using System;
using System.Collections.Generic;

namespace Demo.Models;

public partial class City
{
    public int Id { get; set; }

    public int? StateId { get; set; }

    public string? FullName { get; set; }

    public int? Pincode { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual ICollection<Registration> Registrations { get; } = new List<Registration>();

    public virtual State? State { get; set; }
}
