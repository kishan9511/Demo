using System;
using System.Collections.Generic;

namespace Demo.Models;

public partial class OurTeam
{
    public int Id { get; set; }

    public string? TeamTitle { get; set; }

    public string? TeanDetails { get; set; }

    public string? TeamMemberLogo { get; set; }

    public string? ShopOwnerSlidshow { get; set; }

    public string? SlidShowLogo { get; set; }

    public bool? Isdeleted { get; set; }

    public string? TeamMamberName { get; set; }

    public string? TeamMamberProfile { get; set; }

    public string? SlidshowWithName { get; set; }
}
