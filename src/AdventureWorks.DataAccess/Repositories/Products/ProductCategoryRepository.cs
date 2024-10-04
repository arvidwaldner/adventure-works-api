using AdventureWorks.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureWorks.DataAccess.Repositories.Products
{
    public interface IProductCategoryRepository : IRepository<ProductCategory>
    {

    }

    public class ProductCategoryRepository : Repository<ProductCategory>, IProductCategoryRepository
    {
        public ProductCategoryRepository(AdventureWorks2022Context context) : base(context)
        {

        }
    }
}
