using System;
using System.Collections.Generic;

namespace PE_wed_part1.DataAccess;

public partial class Instructor
{
    public int InstructorId { get; set; }

    public string? Fullname { get; set; }

    public DateTime? ContractDate { get; set; }

    public bool? IsFulltime { get; set; }

    public int? Department { get; set; }
}
