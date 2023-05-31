using System;
using System.Collections.Generic;

namespace Demo.Models;

public partial class Country
{
    public int Id { get; set; }

    public string? CountryCode { get; set; }

    public string? ShortName { get; set; }

    public string? FullName { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual ICollection<Registration> Registrations { get; } = new List<Registration>();

    public virtual ICollection<State> States { get; } = new List<State>();
}
