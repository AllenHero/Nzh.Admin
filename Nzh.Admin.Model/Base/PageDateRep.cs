using System;
using System.Collections.Generic;
using System.Text;

namespace Nzh.Admin.Model.Base
{
    public class PageDateRep<T> where T : class, new()
    {
        public int code { get; set; }

        public string msg { get; set; }

        public int count { get; set; }

        public int totalPage { get; set; }

        public List<T> data { get; set; }

        public int PageNum { get; set; }

        public int PageSize { get; set; }
    }
}
