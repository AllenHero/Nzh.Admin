using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nzh.Admin.Common.Base
{
    /// <summary>
    /// 配置文件
    /// </summary>
    public class AppSettings
    {
        public ConnectionStrings ConnectionStrings { get; set; }

        public string DefaultDatabase { get; set; }

        public string ComponentDbType { get; set; }
    }


   
    public class ConnectionStrings
    {
        public string SqlServer { get; set; }

        public string MySql { get; set; }

        public string Oracle { get; set; }
    }
}
