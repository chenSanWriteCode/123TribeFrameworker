using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _123TribeFrameworker.Models.QueryModel
{
    public class Query
    {
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime timeBegin { get; set; } = DateTime.Now;
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime timeEnd { get; set; } = DateTime.Now;
    }
}