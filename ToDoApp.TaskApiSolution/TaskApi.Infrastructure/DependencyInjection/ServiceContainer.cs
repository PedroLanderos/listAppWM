using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TaskApi.Application.Interfaces;
using TaskApi.Infrastructure.Data;
using TaskApi.Infrastructure.Repositories;

namespace TaskApi.Infrastructure.DependencyInjection
{
    public static class ServiceContainer
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<TaskDbContext>(x => x.UseSqlServer(config.GetConnectionString("listapiconnection")));
            services.AddScoped<ITask, TaskRepository>();

            return services;
        }
    }
}
