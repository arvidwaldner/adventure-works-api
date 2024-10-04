using AdventureWorks.DataAccess.Models;
using AdventureWorks.DataAccess.Repositories.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureWorks.DataAccess.Repositories.Production
{
    public interface ILocationRepository : IRepository<Location>
    {

    }

    public class LocationRepository : Repository<Location>, ILocationRepository
    {
        public LocationRepository(AdventureWorks2022Context context) : base(context)
        {

        }
    }
}
