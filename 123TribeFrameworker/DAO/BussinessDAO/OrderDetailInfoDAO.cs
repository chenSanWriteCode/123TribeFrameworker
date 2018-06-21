using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using _123TribeFrameworker.DAO.DirDAO;
using _123TribeFrameworker.Entity;
using _123TribeFrameworker.Models.QueryModel;

namespace _123TribeFrameworker.DAO.BussinessDAO
{
    public class OrderDetailInfoDAO : IOrderDetailInfoDAO
    {
        
        /// <summary>
        /// 查询所有
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public List<OrderDetailInfo> searchAllByCondition(OrderDetailInfoQuery t)
        {
            LayerDbContext context = new LayerDbContext();
            var result = context.orderDetailInfo.Where(x => x.id > 0);
            result = string.IsNullOrEmpty(t.orderNo) ? result : result.Where(x => x.orderNo.Contains(t.orderNo));
            result = string.IsNullOrEmpty(t.materialName) ? result : result.Where(x => x.materialInfo.materialName.Contains(t.materialName));
            result = string.IsNullOrEmpty(t.mat_size) ? result : result.Where(x => x.materialInfo.mat_size == t.mat_size);
            result = string.IsNullOrEmpty(t.alias) ? result : result.Where(x => x.materialInfo.alias.Contains(t.alias));
            result = string.IsNullOrEmpty(t.remark) ? result : result.Where(x => x.materialInfo.remark.Contains(t.remark));
            result = string.IsNullOrEmpty(t.status) ? result : result.Where(x => x.orderInfo.status == t.status);
            result = !t.createdDateBegin.HasValue ? result : result.Where(x => x.createdDate >= t.createdDateBegin);
            result = !t.createdDateEnd.HasValue ? result : result.Where(X => X.createdDate <= t.createdDateEnd);
            result = !t.receivedDateBegin.HasValue ? result : result.Where(x => x.orderInfo.receivedDate >= t.receivedDateBegin);
            result = !t.receivedDateEnd.HasValue ? result : result.Where(x => x.orderInfo.receivedDate <= t.receivedDateEnd);
            return result.ToList();
        }
        /// <summary>
        /// 根据pager
        /// </summary>
        /// <param name="pager"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public List<OrderDetailInfo> searchByCondition(Pager<List<OrderDetailInfo>> pager, OrderDetailInfoQuery t)
        {
            LayerDbContext context = new LayerDbContext();
            int start = (pager.page - 1) * pager.recPerPage;
            var result = context.orderDetailInfo.Where(x => x.id > 0);
            result = string.IsNullOrEmpty(t.orderNo) ? result : result.Where(x => x.orderNo.Contains(t.orderNo));
            result = string.IsNullOrEmpty(t.materialName) ? result : result.Where(x => x.materialInfo.materialName.Contains(t.materialName));
            result = string.IsNullOrEmpty(t.mat_size) ? result : result.Where(x => x.materialInfo.mat_size == t.mat_size);
            result = string.IsNullOrEmpty(t.alias) ? result : result.Where(x => x.materialInfo.alias.Contains(t.alias));
            result = string.IsNullOrEmpty(t.remark) ? result : result.Where(x => x.materialInfo.remark.Contains(t.remark));
            result = string.IsNullOrEmpty(t.status) ? result : result.Where(x => x.orderInfo.status == t.status);
            result = !t.createdDateBegin.HasValue ? result : result.Where(x => x.createdDate >= t.createdDateBegin);
            result = !t.createdDateEnd.HasValue ? result : result.Where(X => X.createdDate <= t.createdDateEnd);
            result = !t.receivedDateBegin.HasValue ? result : result.Where(x => x.orderInfo.receivedDate >= t.receivedDateBegin);
            result = !t.receivedDateEnd.HasValue ? result : result.Where(x => x.orderInfo.receivedDate <= t.receivedDateEnd);
            result = result.OrderBy(x => x.orderNo);
            result = result.Skip(start).Take(pager.recPerPage);
            return result.ToList();
        }
        /// <summary>
        /// 查询数量
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public int searchCountByCondition(OrderDetailInfoQuery t)
        {
            LayerDbContext context = new LayerDbContext();
            var result = context.orderDetailInfo.Where(x => x.id > 0);
            result = string.IsNullOrEmpty(t.orderNo) ? result : result.Where(x => x.orderNo.Contains(t.orderNo));
            result = string.IsNullOrEmpty(t.materialName) ? result : result.Where(x => x.materialName.Contains(t.materialName));
            result = string.IsNullOrEmpty(t.mat_size) ? result : result.Where(x => x.materialInfo.mat_size == t.mat_size);
            result = string.IsNullOrEmpty(t.alias) ? result : result.Where(x => x.materialInfo.alias.Contains(t.alias));
            result = string.IsNullOrEmpty(t.remark) ? result : result.Where(x => x.materialInfo.remark.Contains(t.remark));
            result = string.IsNullOrEmpty(t.status) ? result : result.Where(x => x.orderInfo.status == t.status);
            result = !t.createdDateBegin.HasValue ? result : result.Where(x => x.createdDate >= t.createdDateBegin);
            result = !t.createdDateEnd.HasValue ? result : result.Where(X => X.createdDate <= t.createdDateEnd);
            result = !t.receivedDateBegin.HasValue ? result : result.Where(x => x.orderInfo.receivedDate >= t.receivedDateBegin);
            result = !t.receivedDateEnd.HasValue ? result : result.Where(x => x.orderInfo.receivedDate <= t.receivedDateEnd);
            return result.Count();
        }
        /// <summary>
        /// 收货订单
        /// </summary>
        /// <param name="list"></param>
        /// <param name="userName"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        /// <remarks>
        /// 1. 增加库存，2. 增加入库记录 3. 修改订单状态
        /// </remarks>
        public async Task<Result<int>> receiveOrder(List<InStorageRecord> list, string userName, OrderStatusEnum status)
        {
            Result<int> result = new Result<int>();
            string orderNo = list.First().orderNo;
            using (LayerDbContext context = new LayerDbContext())
            {
                try
                {
                    var model = context.orderInfo.Where(x => x.orderNo == orderNo).Single();
                    if (model.status == status.ToString())
                    {
                        result.addError("进货单已被" + model.receivedBy + "收货");
                    }
                    else
                    {
                        //订单状态
                        model.status = status.ToString();
                        model.receivedDate = DateTime.Now;
                        model.receivedBy = userName;
                        model.sumPriceReal = list.Sum(x => x.countReal * x.priceIn);
                        //入库记录
                        context.inStorageRecord.AddRange(list);
                        //库存
                        List<Inventory> inventoryList = new List<Inventory>();
                        Inventory model1 = null;
                        foreach (var item in list)
                        {
                            model1 = new Inventory()
                            {
                                count = item.countReal,
                                materialId = item.materialId,
                                priceIn = item.priceIn
                            };
                            inventoryList.Add(model1);
                        }
                        context.inventory.AddRange(inventoryList);
                        
                        await context.SaveChangesAsync();
                    }
                }
                catch (Exception err)
                {
                    result.addError(err.Message);
                }
            }
            return result;
        }
        /// <summary>
        /// 处理异常，补充收货
        /// </summary>
        /// <param name="supplementList">补充产品list</param>
        /// <param name="userName">操作人</param>
        /// <returns></returns>
        public async Task<Result<int>> supplementReceiveOrder(List<InStorageRecord> supplementList, string userName)
        {
            Result<int> result = new Result<int>();
            string orderNo = supplementList.First().orderNo;
            using (LayerDbContext context = new LayerDbContext())
            {
                try
                {
                    //1.插入入库记录  2.查询本订单下 根据物料group by  应收与实收是否相等，修改订单状态与订单实际总成本   3. 增加库存
                    var orderModel = context.orderInfo.Where(x => x.orderNo == orderNo).Single();
                    context.inStorageRecord.AddRange(supplementList);

                    //入库记录中 本订单 各物料的应收实收数量
                    var inStorageRecodes = context.inStorageRecord.Where(x => x.orderNo == orderNo).GroupBy(x => x.materialId).Select(x => new { materialId = x.Key, countReference = x.Max(item => item.countReference), countReal = x.Sum(item => item.countReal), priceIn = x.Max(item => item.priceIn) }).ToList();
                    List<InStorageRecord> inStorageRecodeCopy = new List<InStorageRecord>();
                    InStorageRecord model = null;
                    foreach (var item in inStorageRecodes)
                    {
                        model = new InStorageRecord();
                        model.materialId = item.materialId;
                        model.priceIn = item.priceIn;
                        model.countReal = item.countReal;
                        model.countReference = item.countReference;
                        inStorageRecodeCopy.Add(model);
                    }
                    //订单状态
                    OrderStatusEnum status = OrderStatusEnum.completed;
                    //实际进价总和
                    float sumPriceReal = 0;
                    foreach (var item in supplementList)
                    {
                        inStorageRecodeCopy.Where(x => x.materialId == item.materialId).Single().countReal += item.countReal;
                    }
                    if (inStorageRecodeCopy.Where(x => x.countReal != x.countReference).Count() > 0)
                    {
                        status = OrderStatusEnum.excepted;
                    }
                    sumPriceReal = inStorageRecodeCopy.Sum(x => x.priceIn * x.countReal);
                    orderModel.status = status.ToString();
                    orderModel.lastUpdatedBy = userName;
                    orderModel.lastUpdatedDate = DateTime.Now;
                    orderModel.sumPriceReal = sumPriceReal;

                    //库存
                    List<Inventory> inventoryList = new List<Inventory>();
                    Inventory model1 = null;
                    foreach (var item in supplementList)
                    {
                        model1 = new Inventory()
                        {
                            count = item.countReal,
                            materialId = item.materialId,
                            priceIn = item.priceIn
                        };
                        inventoryList.Add(model1);
                    }
                    context.inventory.AddRange(inventoryList);

                    await context.SaveChangesAsync();
                }
                catch (Exception err)
                {
                    result.addError(err.Message);
                }
            }
            return result;
        }
        public Task<OrderDetailInfo> searchById(int id)
        {
            throw new NotImplementedException();
        }
        public Task<Result<int>> add(OrderDetailInfo t)
        {
            throw new NotImplementedException();
        }
        public Task<Result<int>> deleteById(int id)
        {
            throw new NotImplementedException();
        }
        public Task<Result<int>> update(OrderDetailInfo t)
        {
            throw new NotImplementedException();
        }

        
    }
}