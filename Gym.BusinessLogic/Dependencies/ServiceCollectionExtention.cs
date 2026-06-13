using Gym.BusinessLogic.Services;
using Gym.DataAccess.Data.Context;
using Gym.DataAccess.Interceptor;
using Gym.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gym.BusinessLogic.Dependencies
{
    public static class ServiceCollectionExtention 
    {
        public static IServiceCollection AddBusinessLogic(this IServiceCollection services)
        {
            services.AddScoped<IMemberService, MemberService>();
            services.AddScoped<ISessionService, SessionService>();
            services.AddScoped<ITrainerService, TrainerService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
