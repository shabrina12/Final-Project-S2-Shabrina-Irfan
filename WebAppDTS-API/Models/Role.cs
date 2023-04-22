using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WebAppDTS_API.Models;

public partial class Role
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<AccountRole> AccountRoles { get; } = new List<AccountRole>();
}
