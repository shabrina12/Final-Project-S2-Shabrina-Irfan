using System;
using System.Collections.Generic;

namespace WebAppDTS_API.Models;

public partial class Account
{
    public string EmployeeNik { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual ICollection<AccountRole>? AccountRoles { get; } = new List<AccountRole>();

    public virtual Employee EmployeeNikNavigation { get; set; } = null!;
}
