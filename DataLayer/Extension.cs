using DataLayer.DbContext;
using DataLayer.Interfaces;
using DataLayer.Repositories;
using DataLayer.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public static class Extension
    {
        public static IServiceCollection AddMySqlCoreDb(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IMySqlDbContextOptionsFactory, MySqlDbContextOption>();
            services.AddTransient<IEfContextFactory, MainDbContextFactory>();
            services.AddTransient<IUnitOfWorkRepositories, UnitOfWorkRepositories>();

            return services;
        }
    }
}
