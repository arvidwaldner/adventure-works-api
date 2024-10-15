using AdventureWorks.DataAccess.Models;

namespace AdventureWorks.DataAccess.Repositories.HumanResources
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
