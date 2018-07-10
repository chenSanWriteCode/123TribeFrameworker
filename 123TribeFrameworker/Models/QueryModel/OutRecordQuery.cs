using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _123TribeFrameworker.Models.QueryModel
{
    public class OutRecordQuery
    {
        /// <summary>
        /// 售出订单号 标记哪些货物是一起被卖出的
        /// </summary>
        public string cashOrder { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        public string materialName { get; set; }
        public string mat_size { get; set; }
        public DateTime? createdDateBegin { get; set; }
        public DateTime? createdDateEnd { get; set; }
    }
}