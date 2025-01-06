using System;
using System.Collections.Generic;

namespace TentBooking.Models;

public partial class User
{
    public int UserId { get; set; }

    public string? UserName { get; set; }

    public string? UserEmail { get; set; }

    public string PasswordHash { get; set; } = null!;

    public virtual ICollection<UserClaim> UserClaims { get; } = new List<UserClaim>();

    public virtual ICollection<UserRole> UserRoles { get; } = new List<UserRole>();
}
