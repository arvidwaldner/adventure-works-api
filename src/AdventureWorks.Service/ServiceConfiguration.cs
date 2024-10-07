﻿using AdventureWorks.DataAccess;
using AdventureWorks.Service.HumanResources;
using AdventureWorks.Service.Production;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureWorks.Service
{
    public static class ServiceConfiguration
    {
        public static void ConfigureAdventureWorksServices(IServiceCollection services, IConfiguration configuration)
        {
            AddProductionServices(services, configuration);
            AddHumanResourcesServices(services, configuration);
            ConfigureDataAccess(services, configuration);
        }

        private static void AddProductionServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IProductsService, ProductsService>();
            services.AddTransient<IProductCategoryService, ProductCategoryService>();
            services.AddTransient<ILocationService, LocationService>();
        }

        private static void AddHumanResourcesServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IDepartmentService, DepartmentService>();
        }

        private static void ConfigureDataAccess(IServiceCollection services, IConfiguration configuration)
        {
            DataAccessConfiguration.ConfigureDataAccess(services, configuration);
        }
    }
}
