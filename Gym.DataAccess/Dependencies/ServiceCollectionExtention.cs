using Gym.DataAccess.Data.Context;
using Gym.DataAccess.Interceptor;
using Gym.DataAccess.Queries;
using Gym.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gym.DataAccess.Dependencies
{
    public static class ServiceCollectionExtention 
    {
        public static IServiceCollection AddDataAccess(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<GymDBContext>((sp, options) =>
            {
                options.UseSqlServer(connectionString);

                options.AddInterceptors(
                    sp.GetRequiredService<AuditColumns>());
            });
            services.AddScoped<ISessionQueryService, SessionQueryService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            //services.AddScoped<IPlanRepository, PlanRepository>();
            services.AddScoped<AuditColumns>();

            //services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            //services.AddScoped<IMemberRepository, MemberRepository>();

            return services;
        }
    }
}
