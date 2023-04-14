using System;
using System.Collections.Generic;

namespace WebAppDTS_API.Models;

public partial class Education
{
    public int Id { get; set; }

    public string Major { get; set; } = null!;

    public string Degree { get; set; } = null!;

    public decimal Gpa { get; set; }

    public int UniversityId { get; set; }

    public virtual Profiling? Profiling { get; set; }

    public virtual University University { get; set; } = null!;
}
