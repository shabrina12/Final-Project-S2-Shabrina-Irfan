using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WebAppDTS_API.Models;

public partial class Profiling
{
    public string EmployeeNik { get; set; } = null!;

    public int EducationId { get; set; }
    [JsonIgnore]
    public virtual Education Education { get; set; } = null!;
    [JsonIgnore]
    public virtual Employee EmployeeNikNavigation { get; set; } = null!;
}
