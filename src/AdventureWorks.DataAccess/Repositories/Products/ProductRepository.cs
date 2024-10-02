using AdventureWorks.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureWorks.DataAccess.Repositories.Products
{
    public interface IProductRepository : IRepository<Product>
    {

    }

    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(AdventureWorks2022Context context) : base(context)
        {

        }
    }
}
