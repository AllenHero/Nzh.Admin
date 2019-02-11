using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
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
using Nzh.Admin.Extension;
using Nzh.Admin.IService;
using Nzh.Admin.Model;
using Nzh.Admin.Model.Base;
using Nzh.Admin.SwaggerHelp;
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


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddMvc();

            //注入服务、仓储类
            //services.AddTransient<IDemoRepository, DemoRepository>();
            //services.AddTransient<IDemoService, DemoService>();

             services.AddRepositories();

            services.AddMvc();

            #region Swagger

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1.1.0",
                    Title = "Nzh.Admin WebAPI",
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
            });
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
