using System;
using System.Collections.Generic;

namespace Demo.Models;

public partial class Registration
{
    public int Id { get; set; }

    public int? CountryId { get; set; }

    public int? StateId { get; set; }

    public int? CityId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Gender { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public string? Occupation { get; set; }

    public DateTime? Dob { get; set; }

    public int? Uidnumber { get; set; }

    public string? VoteId { get; set; }

    public string? PassportNumber { get; set; }

    public string? PanCardId { get; set; }

    public string? Qualification { get; set; }

    public string? Address { get; set; }

    public bool? IsActive { get; set; }

    public string? Image { get; set; }

    public bool? IsDeleted { get; set; }

    public int? RoleId { get; set; }

    public virtual City? City { get; set; }

    public virtual Country? Country { get; set; }

    public virtual Role? Role { get; set; }

    public virtual State? State { get; set; }
}
