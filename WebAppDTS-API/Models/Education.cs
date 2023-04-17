using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WebAppDTS_API.Models;

public partial class Education
{
    public int Id { get; set; }

    public string Major { get; set; } = null!;

    public string Degree { get; set; } = null!;

    public decimal Gpa { get; set; }

    public int UniversityId { get; set; }
    [JsonIgnore]
    public virtual Profiling? Profiling { get; set; }
    [JsonIgnore]
    public virtual University? University { get; set; } = null!;
}
