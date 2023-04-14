using System;
using System.Collections.Generic;

namespace WebAppDTS_API.Models;

public partial class AccountRole
{
    public int Id { get; set; }

    public string AccountNik { get; set; } = null!;

    public int RoleId { get; set; }

    public virtual Account AccountNikNavigation { get; set; } = null!;

    public virtual Role Role { get; set; } = null!;
}
