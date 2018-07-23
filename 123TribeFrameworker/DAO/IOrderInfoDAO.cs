using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _123TribeFrameworker.CommonTools;
using _123TribeFrameworker.Entity;
using _123TribeFrameworker.Models.QueryModel;

namespace _123TribeFrameworker.DAO
{
    public interface IOrderInfoDAO:ICommonDAO<OrderInfo>
    {
        Task<OrderInfo> searchByOrder(string orderNo);

        List<OrderInfo> searchByCondition(Pager<List<OrderInfo>> pager, OrderInfoQuery t);

        int searchCountByCondition(OrderInfoQuery t);

        Task<Result<int>> deleteByOrderNo(string orderNo);
        /// <summary>
        /// 创建订单
        /// </summary>
        /// <param name="order">订单</param>
        /// <param name="orderList">订单详情</param>
        /// <returns></returns>
        Task<Result<List<OrderDetailInfo>>> addOrder(OrderInfo order, List<OrderDetailInfo> orderList);

        OrderTool getHalfYearOrderNum();


    }
}
