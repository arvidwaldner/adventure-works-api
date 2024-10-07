using AdventureWorks.DataAccess.Models;
using AdventureWorks.DataAccess.Repositories;
using AdventureWorks.DataAccess.Repositories.Production;
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

            AddGenericRepository(services);
            AddProductionRepositories(services);
            AddHumanResourcesRepositories(services);
        }

        private static void AddGenericRepository(IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        }

        private static void AddProductionRepositories(IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();
            services.AddScoped<ILocationRepository, LocationRepository>();
        }

        private static void AddHumanResourcesRepositories(IServiceCollection services)
        {
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
        }
    }
}
