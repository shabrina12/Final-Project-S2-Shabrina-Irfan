using System;
using System.Collections.Generic;

namespace Web_API.Models;

public partial class Account
{
    public string EmployeeNik { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual Employee EmployeeNikNavigation { get; set; } = null!;

    public virtual ICollection<AccountRole> TbTrAccountRoles { get; set; } = new List<AccountRole>();
}
