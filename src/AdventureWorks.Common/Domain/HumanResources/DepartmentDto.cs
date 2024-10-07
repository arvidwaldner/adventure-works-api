using System;
using System.Collections.Generic;

namespace AdventureWorks.Common.Domain.HumanResources;
public class DepartmentDto
{
    public short DepartmentId { get; set; }
    public string Name { get; set; } = null!;
    public string GroupName { get; set; } = null!;
    public DateTime ModifiedDate { get; set; }    
}
