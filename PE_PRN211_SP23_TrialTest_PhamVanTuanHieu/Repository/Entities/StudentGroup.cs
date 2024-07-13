using System;
using System.Collections.Generic;

namespace Repository.Entities;

public partial class StudentGroup
{
    public int Id { get; set; }

    public string? Code { get; set; }

    public string? GroupName { get; set; }

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
