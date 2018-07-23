using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using _123TribeFrameworker.CommonTools;
using _123TribeFrameworker.DAO;
using _123TribeFrameworker.Entity;
using _123TribeFrameworker.Models.QueryModel;
using Unity.Attributes;

namespace _123TribeFrameworker.Services.Layer
{
    public class OrderInfoService : IOrderInfoService
    {
        [Dependency]
        public IOrderInfoDAO dao { get; set; }
        [Dependency]
        public IOrderDetailInfoService detailService { get; set; }
        
        public Pager<List<OrderInfo>> searchByCondition(Pager<List<OrderInfo>> pager, OrderInfoQuery condition)
        {
            Task<List<OrderInfo>> task = Task.Factory.StartNew(() => dao.searchByCondition(pager, condition));
            pager.data = task.Result;
            Task<int> countTask = Task.Factory.StartNew(() => dao.searchCountByCondition(condition));
            pager.recTotal = countTask.Result;
            return pager;
        }
        public async Task<Result<int>> deleteByOrderNo(string orderNo)
        {
            return await dao.deleteByOrderNo(orderNo); 
        }
        public Task<OrderInfo> searchByOrder(string orderNo)
        {
            return dao.searchByOrder(orderNo);
        }
        public async Task<Result<List<OrderDetailInfo>>> addOrder(List<OrderDetailInfo> orderList, string name, float sumPrice)
        {
            OrderInfo order = new OrderInfo();
            order.createdBy = name;
            order.orderNo = DateTime.Now.ToString("yyyyMMddHHmmss");
            order.status = "generated";
            order.sumPrice = sumPrice;
            foreach (var item in orderList)
            {
                item.orderNo = order.orderNo;
                item.createdBy = order.createdBy;
            }
            return await dao.addOrder(order, orderList);
        }

        public async Task<OrderTool> getHalfYearOrderNum()
        {
            return await Task.Factory.StartNew(() => dao.getHalfYearOrderNum());
        }
    }
}