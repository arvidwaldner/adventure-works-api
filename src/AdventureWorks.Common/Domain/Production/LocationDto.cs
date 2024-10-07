using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureWorks.Common.Domain.Production
{
    public class LocationDto
    {
        public short LocationId { get; set; }
        public string Name { get; set; } = string.Empty!;
        public decimal CostRate { get; set; }
        public decimal Availability { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
