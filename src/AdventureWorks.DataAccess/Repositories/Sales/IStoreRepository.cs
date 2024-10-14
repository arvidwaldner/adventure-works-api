using AdventureWorks.DataAccess.Models;
using AdventureWorks.DataAccess.Repositories.Products;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureWorks.DataAccess.Repositories.Sales
{
    public interface IStoreRepository : IRepository<Store>
    {

    }

    public class StoreRepository : Repository<Store>, IStoreRepository
    {
        public StoreRepository(AdventureWorks2022Context context) : base(context)
        {

        }        
    }
}
