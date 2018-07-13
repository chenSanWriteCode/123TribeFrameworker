using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _123TribeFrameworker.Models.QueryModel
{
    public class InventoryQuery
    {
        /// <summary>
        /// 入库时间
        /// </summary>
        public DateTime createdDate { get; set; }
        public DateTime? createdDateBegin { get; set; }
        public DateTime? createdDateEnd { get; set; }

        public int? materialId { get; set; }
        /// <summary>
        /// 物料名称
        /// </summary>
        public string materialName { get; set; }
        /// <summary>
        /// 别名
        /// </summary>
        public string alias { get; set; }
        /// <summary>
        /// 规格
        /// </summary>
        public string mat_size { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string remark { get; set; }
        /// <summary>
        /// 库存不足标志 
        /// 1足够
        /// 0不足
        /// -1所有
        /// </summary>
        public int lackFlag { get; set; }
    }
}