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

        public Task<Result<int>> deleteByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Pager<List<OrderInfo>> searchByCondition(Pager<List<OrderInfo>> pager, OrderInfoQuery condition)
        {
            Task<List<OrderInfo>> task = Task.Factory.StartNew(() => dao.searchByCondition(pager, condition));
            pager.data = task.Result;
            Task<int> countTask = Task.Factory.StartNew(() => dao.searchCountByCondition(condition));
            pager.recTotal = countTask.Result;
            return pager;
        }

        public Pager<List<OrderInfo>> searchByCondition<QueryT>(Pager<List<OrderInfo>> pager, QueryT condition)
        {
            throw new NotImplementedException();
        }

        public Task<OrderInfo> searchByid(int id)
        {
            throw new NotImplementedException();
        }
        public Task<OrderInfo> searchByOrder(string orderNo)
        {
            return dao.searchByOrder(orderNo);
        }

        public Task<Result<int>> update(OrderInfo model)
        {
            throw new NotImplementedException();
        }
        
    }
}