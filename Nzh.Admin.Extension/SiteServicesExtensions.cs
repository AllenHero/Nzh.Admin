﻿using Microsoft.Extensions.DependencyInjection;
using Nzh.Admin.IRepository;
using Nzh.Admin.IService;
using Nzh.Admin.Repository;
using Nzh.Admin.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nzh.Admin.Extension
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

            return services;
        }
    }
}
