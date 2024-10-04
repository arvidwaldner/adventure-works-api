using AdventureWorks.DataAccess.Models;
using AdventureWorks.DataAccess.Repositories;
using AdventureWorks.DataAccess.Repositories.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureWorks.DataAccess
{
    public static class DataAccessConfiguration
    {
        public static void ConfigureDataAccess(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AdventureWorks2022Context>(options =>
                options.UseSqlServer(configuration.GetConnectionString("AdventureWorksConnection")));

            AddRepositories(services);
        }

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();
        }
    }
}
