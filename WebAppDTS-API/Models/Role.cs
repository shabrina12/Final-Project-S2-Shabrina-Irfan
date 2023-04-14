using System;
using System.Collections.Generic;

namespace WebAppDTS_API.Models;

public partial class Role
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<AccountRole> AccountRoles { get; } = new List<AccountRole>();
}
