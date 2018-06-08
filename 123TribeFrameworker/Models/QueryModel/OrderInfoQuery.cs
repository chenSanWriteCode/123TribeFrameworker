using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _123TribeFrameworker.Models.QueryModel
{
    public class OrderInfoQuery
    {
        public string orderNo { get; set; }
        /// <summary>
        /// 订单状态
        /// </summary>
        public string status { get; set; }
        /// <summary>
        /// 合计进货价格
        /// </summary>
        public double sumPrice { get; set; }
        /// <summary>
        /// 是否打印
        /// </summary>
        public int isPrinted { get; set; }
        /// <summary>
        /// 打印时间
        /// </summary>
        public DateTime? printedTimeBegin { get; set; }
        public DateTime? printedTimeEnd { get; set; }
        /// <summary>
        /// 打印次数
        /// </summary>
        public int printedNum { get; set; }
        public string createdBy { get; set; }
        /// <summary>
        /// 进货单下发时间
        /// </summary>
        public DateTime createdDate { get; set; }
        public DateTime? createdDateBegin { get; set; }
        public DateTime? createdDateEnd { get; set; }
        public DateTime? lastUpdatedDate { get; set; }
        public string lastUpdatedBy { get; set; }
        /// <summary>
        /// 收货时间
        /// </summary>
        public DateTime? receivedDate { get; set; }
        public DateTime? receivedDateBegin { get; set; }
        public DateTime? receivedDateEnd { get; set; }
        
    }
}