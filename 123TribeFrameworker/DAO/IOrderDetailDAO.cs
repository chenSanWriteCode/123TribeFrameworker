using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _123TribeFrameworker.Entity;
using _123TribeFrameworker.Models.QueryModel;

namespace _123TribeFrameworker.DAO
{
    public interface IOrderDetailInfoDAO : ICommonDAO<OrderDetailInfo>
    {
        List<OrderDetailInfo> searchByCondition(Pager<List<OrderDetailInfo>> pager, OrderDetailInfoQuery t);
        List<OrderDetailInfo> searchAllByCondition(OrderDetailInfoQuery t);
        int searchCountByCondition(OrderDetailInfoQuery t);
        /// <summary>
        /// 收货订单
        /// </summary>
        /// <param name="list">订单详情</param>
        /// <param name="userName">收货人</param>
        /// <param name="status">订单状态</param>
        /// <returns></returns>
        Task<Result<int>> receiveOrder(List<InStorageRecord> list, string userName, OrderStatusEnum status);
        /// <summary>
        /// 处理异常，补充收货
        /// </summary>
        /// <param name="supplementList">补充产品list</param>
        /// <param name="userName">操作人</param>
        /// <returns></returns>
        Task<Result<int>> supplementReceiveOrder(List<InStorageRecord> supplementList, string userName);
        Task<Result<int>> deleteOrderDetailById(int id, string userName);
    }
}
