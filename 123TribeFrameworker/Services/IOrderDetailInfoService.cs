using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _123TribeFrameworker.Entity;
using _123TribeFrameworker.Models.QueryModel;

namespace _123TribeFrameworker.Services
{
    public interface IOrderDetailInfoService
    {
        Pager<List<OrderDetailInfo>> searchByCondition(Pager<List<OrderDetailInfo>> pager, OrderDetailInfoQuery condition);
        List<OrderDetailInfo> searchAllByCondition(OrderDetailInfoQuery condition);
        /// <summary>
        /// 订单收货
        /// </summary>
        /// <param name="list">订单详情</param>
        /// <param name="userName">收货人</param>
        /// <returns></returns>
        Task<Result<int>> receiveOrder(List<InStorageRecord> list,string userName);
        Task<Result<int>> deleteByOrderNo(string orderNo);
    }
}
