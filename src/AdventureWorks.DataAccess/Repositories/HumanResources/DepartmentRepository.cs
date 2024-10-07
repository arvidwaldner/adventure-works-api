using AdventureWorks.DataAccess.Models;
using AdventureWorks.DataAccess.Repositories.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureWorks.DataAccess.Repositories.Production
{
    public interface IDepartmentRepository : IRepository<Department>
    {

    }

    public class DepartmentRepository : Repository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(AdventureWorks2022Context context) : base(context)
        {

        }
    }
}
