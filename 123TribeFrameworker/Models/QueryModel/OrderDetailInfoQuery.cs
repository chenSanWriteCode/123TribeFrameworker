using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _123TribeFrameworker.Models.QueryModel
{
    public class OrderDetailInfoQuery
    {
        public string orderNo { get; set; }
        
        /// <summary>
        /// 物料名称
        /// </summary>
        public string materialName { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int num { get; set; }
        /// <summary>
        /// 供货商
        /// </summary>
        public string factory { get; set; }

        public DateTime createdDate { get; set; }
        public DateTime? createdDateBegin { get; set; }
        public DateTime? createdDateEnd { get; set; }

        public string createdBy { get; set; }
        public DateTime? lastUpdatedDate { get; set; }
        public string lastUpdatedBy { get; set; }
        public DateTime? receivedDateBegin { get; set; }
        public DateTime? receivedDateEnd { get; set; }

        public string status { get; set; }

        
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
    }
}