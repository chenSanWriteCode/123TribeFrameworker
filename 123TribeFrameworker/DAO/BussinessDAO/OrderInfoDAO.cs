using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using _123TribeFrameworker.CommonTools;
using _123TribeFrameworker.Entity;
using _123TribeFrameworker.Infrastructrue;
using _123TribeFrameworker.Models.QueryModel;

namespace _123TribeFrameworker.DAO.BussinessDAO
{
    public class OrderInfoDAO : IOrderInfoDAO
    {

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
            result = result.OrderByDescending(x => x.orderNo);
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

        public Task<OrderInfo> searchById(int id)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 根据订单号删除订单
        /// 删除订单主表订单
        /// 删除订单附表订单内容
        /// </summary>
        /// <param name="orderNo"></param>
        /// <returns></returns>
        public async Task<Result<int>> deleteByOrderNo(string orderNo)
        {
            Result<int> result = new Result<int>();
            try
            {
                using (LayerDbContext context = new LayerDbContext())
                {
                    //根据订单与订单状态查询是否存在订单
                    var modelList = context.orderInfo.Where(x => x.orderNo == orderNo).ToList();
                    if (modelList.Count == 0)
                    {
                        result.addError("订单已被删除");
                    }
                    else
                    {
                        var model = modelList[0];
                        if (model.status != "generated")
                        {
                            result.addError("订单状态已改变，无法删除");
                        }
                        else
                        {
                            // 删除订单主表订单
                            context.orderInfo.Remove(model);
                            // 删除订单附表订单内容
                            var sql = context.orderDetailInfo.RemoveRange(context.orderDetailInfo.Where(x => x.orderNo == orderNo));
                        }
                    }
                    var count = await context.SaveChangesAsync();
                }
            }
            catch (Exception err)
            {
                result.addError(err.Message);
            }
            return result;
        }
        /// <summary>
        /// 创建订单
        /// </summary>
        /// <param name="order"></param>
        /// <param name="orderList"></param>
        /// <returns></returns>
        public async Task<Result<List<OrderDetailInfo>>> addOrder(OrderInfo order, List<OrderDetailInfo> orderList)
        {
            Result<List<OrderDetailInfo>> result = new Result<List<OrderDetailInfo>>();
            try
            {
                using (LayerDbContext context = new LayerDbContext())
                {
                    context.orderInfo.Add(order);
                    context.orderDetailInfo.AddRange(orderList);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception err)
            {
                result.addError(err.Message);
            }
            result.data = orderList;
            return result;
        }

        public OrderTool getHalfYearOrderNum()
        {
            DateTime startTime = DateTime.Now.AddMonths(-5).AddDays(1-DateTime.Now.Day).Date;
            List<DateTime> MonthDate = new List<DateTime>();
            MonthDate = BaseDataHelper.getSixMnthDate(DateTime.Now);
            LayerDbContext context = new LayerDbContext();
            //异常订单数
            var exceptOrderTemp = context.orderInfo.Where(x => x.status == "excepted" && x.receivedDate > startTime).GroupBy(x => x.createdDate.Month).Select(x => new { createdDate = x.Max(item => item.createdDate), count = x.Count() }).ToList();
            //所有订单
            var orderTemp = context.orderInfo.Where(x => x.createdDate > startTime).GroupBy(x => x.createdDate.Month).Select(x => new { createdDate = x.Max(item => item.createdDate), count = x.Count() }).ToList();
            //完成订单
            var completedOrderTemp = context.orderInfo.Where(x => x.status == "completed" && x.receivedDate > startTime).GroupBy(x => x.createdDate.Month).Select(x => new { createdDate = x.Max(item => item.createdDate), count = x.Count() }).ToList();

            List<LineDataTool> orderData = new List<LineDataTool>();
            List<LineDataTool> exceptedOrderData = new List<LineDataTool>();
            List<LineDataTool> completedOrderData = new List<LineDataTool>();
            LineDataTool model = null;
            
            foreach (var item in orderTemp)
            {
                model = new LineDataTool();
                model.intData = item.count;
                model.date = item.createdDate.AddDays(1 - item.createdDate.Day).Date;
                orderData.Add(model);
            }
            foreach (var item in exceptOrderTemp)
            {
                model = new LineDataTool();
                model.intData = item.count;
                model.date = item.createdDate.AddDays(1 - item.createdDate.Day).Date;
                exceptedOrderData.Add(model);
            }
            foreach (var item in completedOrderTemp)
            {
                model = new LineDataTool();
                model.intData = item.count;
                model.date = item.createdDate.AddDays(1 - item.createdDate.Day).Date;
                completedOrderData.Add(model);
            }

            var normalData = (from x in MonthDate.OrderBy(x=>x) join y in orderData on x equals y.date into Temp from t in Temp.DefaultIfEmpty() select t == null ? 0 : t.intData).ToArray();
            var exceptData = (from x in MonthDate.OrderBy(x => x) join y in exceptedOrderData on x equals y.date into Temp from t in Temp.DefaultIfEmpty() select t == null ? 0 : t.intData).ToArray();
            var copmpletedData = (from x in MonthDate.OrderBy(x => x) join y in completedOrderData on x equals y.date into Temp from t in Temp.DefaultIfEmpty() select t == null ? 0 : t.intData).ToArray();
            OrderTool result = new OrderTool();
            result.exceptedOrderNum = exceptData;
            result.produceOrderNum = normalData;
            result.completedOrderNum = copmpletedData;
            return result;
        }

        public Task<Result<int>> add(OrderInfo t)
        {
            throw new NotImplementedException();
        }

        public Task<Result<int>> update(OrderInfo t)
        {
            throw new NotImplementedException();
        }

        public Task<Result<int>> deleteById(int id)
        {
            throw new NotImplementedException();
        }

        
    }
}