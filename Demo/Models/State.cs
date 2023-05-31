using System;
using System.Collections.Generic;

namespace Demo.Models;

public partial class State
{
    public int Id { get; set; }

    public int? CountryId { get; set; }

    public string? ShortName { get; set; }

    public string? FullName { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual ICollection<City> Cities { get; } = new List<City>();

    public virtual Country? Country { get; set; }

    public virtual ICollection<Registration> Registrations { get; } = new List<Registration>();
}
