﻿using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nzh.Admin.SwaggerHelp
{
    public class SwaggerDocTag :IDocumentFilter
    {
        /// <summary>
        /// 添加附加注释
        /// </summary>
        /// <param name="swaggerDoc"></param>
        /// <param name="context"></param>
        public void Apply(SwaggerDocument swaggerDoc, DocumentFilterContext context)
        {
            swaggerDoc.Tags = new List<Tag>
            {
                //添加对应的控制器描述 这个是我好不容易在issues里面翻到的
                new Tag { Name = "Values", Description = "测试模块" },
                new Tag { Name = "Demo", Description = "Demo模块" },
            };
        }
    }
}