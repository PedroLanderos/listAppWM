
using ListApi.Application.Interfaces;
using ListApi.Infrastructure.Data;
using ListApi.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListApi.Infrastructure.DependencyInjection
{
    public static class ServiceContainer
    {
        public static IServiceCollection AddInfrastructureService(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config.GetConnectionString("listapiconnection");
            services.AddDbContext<ListDbConext>(x => x.UseSqlServer(connectionString));
            services.AddScoped<IListApi, ListRepository>();

            return services;
        }
    }
}