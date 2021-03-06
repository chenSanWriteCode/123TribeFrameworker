﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _123TribeFrameworker.Models.QueryModel
{
    public class TradingRecordQuery
    {
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime createdDate { get; set; } 
        public DateTime? createdDateBegin { get; set; }
        public DateTime? createdDateEnd { get; set; }

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