using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using _123TribeFrameworker.DAO;
using _123TribeFrameworker.Entity;
using _123TribeFrameworker.Infrastructrue;
using _123TribeFrameworker.Models.QueryModel;
using Unity.Attributes;

namespace _123TribeFrameworker.Services.Layer
{
    public class OrderDetailInfoService : IOrderDetailInfoService
    {
        [Dependency]
        public IOrderDetailInfoDAO dao { get; set; }
        /// <summary>
        /// 查询页
        /// </summary>
        /// <param name="pager"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public Pager<List<OrderDetailInfo>> searchByCondition(Pager<List<OrderDetailInfo>> pager, OrderDetailInfoQuery condition)
        {
            pager.data = dao.searchByCondition(pager, condition);
            pager.recTotal = dao.searchCountByCondition(condition);
            return pager;
        }
        /// <summary>
        /// 查询所有记录 不分页
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public List<OrderDetailInfo> searchAllByCondition(OrderDetailInfoQuery condition)
        {
            return dao.searchAllByCondition(condition);
        }
        /// <summary>
        /// 订单收货
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public async Task<Result<int>> receiveOrder(List<InStorageRecord> list, string userName)
        {
            OrderStatusEnum status = OrderStatusEnum.completed;
            if (list.Where(x => x.countReference != x.countReal).Count() > 0)
            {
                status = OrderStatusEnum.excepted;
            }
            return await dao.receiveOrder(list, userName, status);
        }
        public Task<Result<int>> deleteByOrderNo(string orderNo)
        {
            throw new NotImplementedException();
        }
    }
}