using System;
using System.Collections.Generic;

namespace Web_API.Models;

public partial class University
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    [System.Text.Json.Serialization.JsonIgnore]
    public virtual ICollection<Education>? TbMEducations { get; set; }
}
