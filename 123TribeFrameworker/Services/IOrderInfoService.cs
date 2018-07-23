using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _123TribeFrameworker.CommonTools;
using _123TribeFrameworker.Entity;
using _123TribeFrameworker.Models.QueryModel;

namespace _123TribeFrameworker.Services
{
    public interface IOrderInfoService 
    {
        Task<OrderInfo> searchByOrder(string orderNo);
        Pager<List<OrderInfo>> searchByCondition(Pager<List<OrderInfo>> pager, OrderInfoQuery condition);

        Task<Result<int>> deleteByOrderNo(string orderNo);
        /// <summary>
        /// 创建订单
        /// </summary>
        /// <param name="orderList">订单详细列表</param>
        /// <param name="name">创建人</param>
        /// <param name="sumPrice">订单总价</param>
        /// <returns></returns>
        Task<Result<List<OrderDetailInfo>>> addOrder(List<OrderDetailInfo> orderList, string name, float sumPrice);

        Task<OrderTool> getHalfYearOrderNum();
    }
}
