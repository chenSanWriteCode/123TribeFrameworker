using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using _123TribeFrameworker.Entity;

namespace _123TribeFrameworker.DAO.BussinessDAO
{
    public class OrderInfoDAO : IOrderInfoDAO
    {
        public Task<int> add(OrderInfo t)
        {
            throw new NotImplementedException();
        }

        public Task<int> deleteById(int id)
        {
            throw new NotImplementedException();
        }

        public List<OrderInfo> searchByCondition(Pager<List<OrderInfo>> pager, OrderInfo t)
        {
            LayerDbContext context = new LayerDbContext();
            int start = (pager.page - 1) * pager.recPerPage;
            var result = context.orderInfo.Where(x => x.id > 0);
            result = string.IsNullOrEmpty(t.orderNo) ? result : result.Where(x => x.orderNo.Contains(t.orderNo));
            result = string.IsNullOrEmpty(t.status) ? result : result.Where(x => x.status == t.status);
            result = !t.createdDateBegin.HasValue ? result : result.Where(x => x.createdDate >= t.createdDateBegin);
            result = !t.createdDateEnd.HasValue ? result : result.Where(x => x.createdDate <= t.createdDateEnd);
            result = !t.receivedDateBegin.HasValue ? result : result.Where(x => x.receivedDate >= t.receivedDateBegin);
            result = !t.receivedDateEnd.HasValue ? result : result.Where(x => x.receivedDate <= t.receivedDateEnd);
            result = result.OrderBy(x => x.orderNo);
            result = result.Skip(start).Take(pager.recPerPage);
            return result.ToList();
        }

        public Task<OrderInfo> searchById(int id)
        {
            throw new NotImplementedException();
        }
        public async Task<OrderInfo> searchByOrder(string orderNo)
        {
            LayerDbContext context = new LayerDbContext();
            OrderInfo result = await context.orderInfo.FindAsync(orderNo);
            return result;
        }

        public int searchCountByCondition(OrderInfo t)
        {
            LayerDbContext context = new LayerDbContext();
            var result = context.orderInfo.Where(x => x.id > 0);
            result = string.IsNullOrEmpty(t.orderNo) ? result : result.Where(x => x.orderNo.Contains(t.orderNo));
            result = string.IsNullOrEmpty(t.status) ? result : result.Where(x => x.status == t.status);
            result = !t.createdDateBegin.HasValue ? result : result.Where(x => x.createdDate >= t.createdDateBegin);
            result = !t.createdDateEnd.HasValue ? result : result.Where(x => x.createdDate <= t.createdDateEnd);
            result = !t.receivedDateBegin.HasValue ? result : result.Where(x => x.receivedDate >= t.receivedDateBegin);
            result = !t.receivedDateEnd.HasValue ? result : result.Where(x => x.receivedDate <= t.receivedDateEnd);
            return result.Count();
        }

        public Task<int> update(OrderInfo t)
        {
            throw new NotImplementedException();
        }
    }
}