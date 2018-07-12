using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _123TribeFrameworker.Models.BussinessModels
{
    public class OutRecordModel
    {
        /// <summary>
        /// 售出订单号 标记哪些货物是一起被卖出的
        /// </summary>
        public string cashOrder { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public float count { get; set; } 
        /// <summary>
        /// 交易人
        /// </summary>
        public string createdBy { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime createdDate { get; set; }
        /// <summary>
        /// 成本总价
        /// </summary>
        public float priceIn { get; set; }
        /// <summary>
        /// 零售单价
        /// </summary>
        public float priceOut { get; set; }
        /// <summary>
        /// 利润
        /// </summary>
        public float profit { get; set; }

        /// <summary>
        /// 物料id
        /// </summary>
        public int materialId { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        public string materialName { get; set; }
        public string alais { get; set; }
        public string mat_size { get; set; }
        public string unit { get; set; }
    }
}