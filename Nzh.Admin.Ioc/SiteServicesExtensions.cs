using Microsoft.Extensions.DependencyInjection;
using Nzh.Admin.IRepository;
using Nzh.Admin.IRepository.Sys;
using Nzh.Admin.IService;
using Nzh.Admin.IService.Sys;
using Nzh.Admin.Repository;
using Nzh.Admin.Repository.Sys;
using Nzh.Admin.Service;
using Nzh.Admin.Service.Sys;
using System;

namespace Nzh.Admin.Ioc
{
    public static class SiteServicesExtensions
    {
        /// <summary>
        /// 注入服务、仓储类
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services.AddScoped<IDemoRepository, DemoRepository>();
            services.AddScoped<IDemoService, DemoService>();

            services.AddScoped<ILogRepository, LogRepository>();
            services.AddScoped<ILogService, LogService>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}
