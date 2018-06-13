using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using _123TribeFrameworker.Entity;
using _123TribeFrameworker.Models.QueryModel;

namespace _123TribeFrameworker.DAO.BussinessDAO
{
    public class OrderInfoDAO : IOrderInfoDAO
    {
        public async Task<int> add(OrderInfo t)
        {
            LayerDbContext context = new LayerDbContext();
            context.orderInfo.Add(t);
            return await context.SaveChangesAsync();
        }
        /// <summary>
        /// 修改订单状态
        /// </summary>
        /// <param name="orderNo"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public async Task<Result<int>> changeOrderStatus(string orderNo,string userName, OrderStatusEnum status)
        {
            LayerDbContext context = new LayerDbContext();
            Result<int> result = new Result<int>();
            try
            {
                var model = context.orderInfo.Where(x => x.orderNo == orderNo).Single();
                if (model.status==status.ToString())
                {
                    result.addError("进货单已被"+model.receivedBy+"收货");
                }
                else
                {
                    model.status = status.ToString();
                    model.receivedDate = DateTime.Now;
                    model.receivedBy = userName;
                    var count = await context.SaveChangesAsync();
                    if (count == 0)
                    {
                        result.addError("修改进货单状态失败，请稍后操作");
                    }
                }
            }
            catch (Exception)
            {
                result.addError("未找到进货单号");
            }
            return result;
        }

        public List<OrderInfo> searchByCondition(Pager<List<OrderInfo>> pager, OrderInfoQuery t)
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
        
        public async Task<OrderInfo> searchByOrder(string orderNo)
        {
            LayerDbContext context = new LayerDbContext();
            OrderInfo result = await context.orderInfo.FindAsync(orderNo);
            return result;
        }

        public int searchCountByCondition(OrderInfoQuery t)
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
        public Task<int> deleteById(int id)
        {
            throw new NotImplementedException();
        }
        public Task<OrderInfo> searchById(int id)
        {
            throw new NotImplementedException();
        }

        
    }
}