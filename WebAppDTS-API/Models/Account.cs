using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WebAppDTS_API.Models;

public partial class Account
{
    public string EmployeeNik { get; set; } = null!;

    public string Password { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<AccountRole> AccountRoles { get; } = new List<AccountRole>();
    [JsonIgnore]
    public virtual Employee EmployeeNikNavigation { get; set; } = null!;
}
