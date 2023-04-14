using System;
using System.Collections.Generic;

namespace WebAppDTS_API.Models;

public partial class Profiling
{
    public string EmployeeNik { get; set; } = null!;

    public int EducationId { get; set; }

    public virtual Education Education { get; set; } = null!;

    public virtual Employee EmployeeNikNavigation { get; set; } = null!;
}
