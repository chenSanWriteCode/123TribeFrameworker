using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
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
        public async Task<Result<OrderInfo>> addAsync(OrderInfo model)
        {
            Result<OrderInfo> result = new Result<OrderInfo>();
            var count = await dao.add(model);
            if (count!=1)
            {
                result.result = false;
                result.message = "订单生成失败";
            }
            return result;
        }

        public async Task<Result<int>> changeOrderStatus(string orderNo, string userName, OrderStatusEnum status)
        {
            return await dao.changeOrderStatus(orderNo, userName, status);
        }


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
            Result<int> result = new Result<int>();
            var resultPart1 = await detailService.deleteByOrderNo(orderNo);
            if (!resultPart1.result)
            {
                result.addError(resultPart1.message);
                return result;
            }
            var model = await dao.searchByOrder(orderNo);
            if (model==null)
            {
                result.addError("订单已被删除");
            }
            else
            {
                if (model.status!="generated")
                {
                    result.addError("订单状态已改变，无法删除");
                }
                else
                {
                    var count = await dao.deleteByOrderNo(orderNo);
                }
            }
            return result;
        }

        public Task<OrderInfo> searchByOrder(string orderNo)
        {
            return dao.searchByOrder(orderNo);
        }


        public Pager<List<OrderInfo>> searchByCondition<QueryT>(Pager<List<OrderInfo>> pager, QueryT condition)
        {
            throw new NotImplementedException();
        }

        public Task<OrderInfo> searchByid(int id)
        {
            throw new NotImplementedException();
        }
       

        public Task<Result<int>> update(OrderInfo model)
        {
            throw new NotImplementedException();
        }
        public Task<Result<int>> deleteByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<List<OrderDetailInfo>>> addOrder(List<OrderDetailInfo> orderList, string name, float sumPrice)
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
    }
}