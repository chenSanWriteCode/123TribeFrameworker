using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _123TribeFrameworker.Models.BussinessModels
{
    public class SaleModel
    {
        /// <summary>
        /// 售出订单号 标记哪些货物是一起被卖出的
        /// </summary>
        public string cashOrder { get; set; }
        public int materialId { get; set; }
        public string materialName { get; set; }
        /// <summary>
        /// 售价
        /// </summary>
        public float priceOut { get; set; }
        /// <summary>
        /// 进价
        /// </summary>
        public float priceIn { get; set; }
        /// <summary>
        /// 折扣
        /// </summary>
        public float discount { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public float count { get; set; }
        public string createdBy { get; set; }
    }
}