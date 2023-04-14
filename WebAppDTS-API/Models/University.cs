using System;
using System.Collections.Generic;

namespace WebAppDTS_API.Models;

public partial class University
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Education> Educations { get; } = new List<Education>();
}
