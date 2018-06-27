using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _123TribeFrameworker.Models.BussinessModels
{
    public class InStorageRecordModel
    {
        public int id { get; set; }
        /// <summary>
        /// 物料id
        /// </summary>
        public int materialId { get; set; }
        /// <summary>
        /// 进货单号
        /// </summary>
        public string orderNo { get; set; }
        /// <summary>
        /// 成本价
        /// </summary>
        public float priceIn { get; set; }
        /// <summary>
        /// 应收入数量
        /// </summary>
        public float countReference { get; set; }
        /// <summary>
        /// 实际收入数量
        /// </summary>
        public float countReal { get; set; }
        /// <summary>
        /// 回执单编号
        /// </summary>
        public string receivedOrder { get; set; }
        /// <summary>
        /// 入库时间
        /// </summary>
        public DateTime createdDate { get; set; } = DateTime.Now;
        /// <summary>
        /// 入库人
        /// </summary>
        public string createdBy { get; set; }


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
    }
}