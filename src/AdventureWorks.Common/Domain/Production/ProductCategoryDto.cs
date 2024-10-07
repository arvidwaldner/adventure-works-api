using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureWorks.Common.Domain.Products
{
    public class ProductCategoryDto
    {
        public int ProductCategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
        public Guid Rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
