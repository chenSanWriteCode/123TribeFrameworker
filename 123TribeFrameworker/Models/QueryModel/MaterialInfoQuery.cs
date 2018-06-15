using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _123TribeFrameworker.Models.QueryModel
{
    public class MaterialInfoQuery
    {
        /// <summary>
        /// 产品编码
        /// </summary>
        public string material { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        public string materialName { get; set; }
        /// <summary>
        /// 别名
        /// </summary>
        public string alias { get; set; }
        /// <summary>
        /// 型号
        /// </summary>
        public string mat_type { get; set; }
        /// <summary>
        /// 规格
        /// </summary>
        public string mat_size { get; set; }
        /// <summary>
        /// 颜色
        /// </summary>
        public string mat_color { get; set; }
        /// <summary>
        /// 重量
        /// </summary>
        public float weight { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        public string unit { get; set; }
        /// <summary>
        /// 参考进货价格
        /// </summary>
        public float referencePriceIn { get; set; }
        /// <summary>
        /// 参考销售价格
        /// </summary>
        public float referencePriceOut { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string remark { get; set; }
    }
}