using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.IdentityModel.Tokens;
using Nzh.Admin.Common.Base;
using Nzh.Admin.Http;
using Nzh.Admin.IRepository;
using Nzh.Admin.IService;
using Nzh.Admin.Model;
using Nzh.Admin.Model.Base;
using Nzh.Admin.Repository;
using Nzh.Admin.Service;
using Nzh.Admin.SwaggerHelp;
using Nzh.Admin.Unit;
using Swashbuckle.AspNetCore.Swagger;

namespace Nzh.Admin
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public IContainer ApplicationContainer { get; private set; }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddMvc();

            #region JWT认证

            services.Configure<JwtSettings>(Configuration.GetSection("JwtSettings"));
            JwtSettings setting = new JwtSettings();
            Configuration.Bind("JwtSettings", setting);
            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(config =>
            {
                config.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidAudience = setting.Audience,
                    ValidIssuer = setting.Issuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(setting.SecretKey))
                };
            });

            #endregion

            //注入服务、仓储类
            services.AddTransient<IDemoRepository, DemoRepository>();
            services.AddTransient<IDemoService, DemoService>();
            services.AddMvc();

            #region Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1.1.0",
                    Title = "STD WebAPI",
                    Description = ".NetCore WebAPI框架",
                    TermsOfService = "None",
                    Contact = new Swashbuckle.AspNetCore.Swagger.Contact { Name = "STD", Email = "", Url = "" }
                });
                //添加注释服务
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, "Nzh.Admin.xml");
                var modelPath = Path.Combine(basePath, "Nzh.Admin.Common.xml");
                var comonPath = Path.Combine(basePath, "Nzh.Admin.Model.xml");
                c.IgnoreObsoleteActions();
                c.DocInclusionPredicate((docName, description) => true);
                c.DescribeAllEnumsAsStrings();
                c.IncludeXmlComments(xmlPath);
                c.IncludeXmlComments(modelPath);
                c.IncludeXmlComments(comonPath);
                //添加对控制器的标签(描述)
                c.DocumentFilter<SwaggerDocTag>();
                //c.OperationFilter<HttpHeaderOperation>(); // 添加httpHeader参数
            });
            #endregion

            #region 依赖注入

            var builder = new ContainerBuilder();//实例化容器
            //注册所有模块module
            builder.RegisterAssemblyModules(Assembly.GetExecutingAssembly());
            //获取所有的程序集
            //var assemblys = BuildManager.GetReferencedAssemblies().Cast<Assembly>().ToArray();
            var assemblys = RuntimeHelper.GetAllAssemblies().ToArray();
            //注册所有继承IDependency接口的类
            builder.RegisterAssemblyTypes().Where(type => typeof(IDependency).IsAssignableFrom(type) && !type.IsAbstract);
            //注册仓储，所有IRepository接口到Repository的映射
            builder.RegisterAssemblyTypes(assemblys).Where(t => t.Name.EndsWith("Repository") && !t.Name.StartsWith("I")).AsImplementedInterfaces();
            //注册服务，所有IApplicationService到ApplicationService的映射
            //builder.RegisterAssemblyTypes(assemblys).Where(t => t.Name.EndsWith("AppService") && !t.Name.StartsWith("I")).AsImplementedInterfaces();
            builder.Populate(services);
            ApplicationContainer = builder.Build();

            return new AutofacServiceProvider(ApplicationContainer); //第三方IOC接管 core内置DI容器 
            //return services.BuilderInterceptableServiceProvider(builder => builder.SetDynamicProxyFactory());
            #endregion

        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();

            app.UseMvc();

            #region Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiHelp V1");
            });
            #endregion
        }
    }
}
