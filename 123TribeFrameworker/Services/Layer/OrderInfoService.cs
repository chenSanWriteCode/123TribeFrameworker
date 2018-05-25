using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using _123TribeFrameworker.DAO;
using _123TribeFrameworker.Entity;
using Unity.Attributes;

namespace _123TribeFrameworker.Services.Layer
{
    public class OrderInfoService : IOrderInfoService
    {
        [Dependency]
        public IOrderInfoDAO dao { get; set; }
        public Task<Result<OrderInfo>> addAsync(OrderInfo model)
        {
            throw new NotImplementedException();
        }

        public Task<Result<int>> deleteByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Pager<List<OrderInfo>> searchByCondition(Pager<List<OrderInfo>> pager, OrderInfo condition)
        {
            Task<List<OrderInfo>> task = Task.Factory.StartNew(() => dao.searchByCondition(pager, condition));
            pager.data = task.Result;
            Task<int> countTask = Task.Factory.StartNew(() => dao.searchCountByCondition(condition));
            pager.recTotal = countTask.Result;
            return pager;
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