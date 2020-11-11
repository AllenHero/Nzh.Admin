﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Nzh.Admin.Model.Filter
{
    public class SearchFilter
    {
        public int pageIndex { get; set; }

        public int pageSize { get; set; }

        public string returnFields { get; set; }

        public string where { get; set; }

        public string orderBy { get; set; }

        public object param { get; set; }

        public IDbTransaction transaction { get; set; }

        public int? commandTimeout { get; set; }
    }
}
