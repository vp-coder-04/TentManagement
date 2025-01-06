using System;
using System.Collections.Generic;

namespace TentBooking.Models;

public partial class Claim
{
    public int ClaimsId { get; set; }

    public string? ClaimType { get; set; }

    public string? ClaimValue { get; set; }

    public virtual ICollection<UserClaim> UserClaims { get; } = new List<UserClaim>();
}
