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
        [Dependency]
        public IInStorageRecordService InStorageservice { get; set; }
        [Dependency]
        public IOrderInfoService Orderservice { get; set; }

        /// <summary>
        /// 生成订单
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public async Task<Result<List<OrderDetailInfo>>> addRange(List<OrderDetailInfo> list)
        {
            Result<List<OrderDetailInfo>> result = new Result<List<OrderDetailInfo>>();
            if (list.Count > 0)
            {
                var count = await dao.addRange(list);
                if (count < list.Count)
                {
                    result.result = false;
                    result.message = "存在生成失败项，请检查";
                }
            }
            return result;
        }

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
        public async Task<Result<int>> receiveOrder(List<InStorageRecord> list,string userName)
        {
            Result<int> result = new Result<int>();
            //增加入库记录与库存
            if (list.Count > 0)
            {
                var resultPart1 = await InStorageservice.addRangeAsync(list);
                if (!resultPart1.result)
                {
                    result.addError(result.message);
                }
                else
                {
                    Result<int> resultPart2 = new Result<int>();
                    //判断是否存在收货异常  应收！=实收  更改订单状态
                    if (list.Where(x => x.countReference != x.countReal).Count() > 0)
                    {
                        resultPart2 =await Orderservice.changeOrderStatus(list[0].orderNo,userName, OrderStatusEnum.excepted);
                    }
                    else
                    {
                        resultPart2 = await Orderservice.changeOrderStatus(list[0].orderNo,userName, OrderStatusEnum.completed);
                    }
                    if (!resultPart2.result)
                    {
                        result.addError(resultPart2.message);
                    }
                }
            }
            return result;
        }
        public Task<OrderDetailInfo> searchByid(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<int>> update(OrderDetailInfo model)
        {
            throw new NotImplementedException();
        }
        public Task<Result<int>> deleteByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
        public Task<Result<OrderDetailInfo>> addAsync(OrderDetailInfo model)
        {
            throw new NotImplementedException();
        }


    }
}