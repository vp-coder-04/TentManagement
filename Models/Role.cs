﻿using System;
using System.Collections.Generic;

namespace TentBooking.Models;

public partial class Role
{
    public int RoleId { get; set; }

    public string? RoleName { get; set; }

    public virtual ICollection<UserRole> UserRoles { get; } = new List<UserRole>();
}
