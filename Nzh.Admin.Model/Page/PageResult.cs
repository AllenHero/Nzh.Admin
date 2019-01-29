using System;
using System.Collections.Generic;
using System.Text;

namespace Nzh.Admin.Model.Page
{
    public class PageResult<T>
    {
        public PageResult()
        {

        }

        /// <summary>
        /// 总记录数
        /// </summary>
        public int total { get; set; }

        /// <summary>
        /// 一页展示的条数
        /// </summary>
        public int page_size { get; set; }

        /// <summary>
        /// 当前页
        /// </summary>
        public int page_num { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public List<T> list { get; set; }
    }
}
