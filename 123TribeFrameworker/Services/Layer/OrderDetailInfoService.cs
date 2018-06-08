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
    public class OrderDetailInfoService : IOrderDetailInfoService
    {
        [Dependency]
        public IOrderDetailInfoDAO dao { get; set; }
        public Task<Result<OrderDetailInfo>> addAsync(OrderDetailInfo model)
        {
            throw new NotImplementedException();
        }

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

        public Task<Result<int>> deleteByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Pager<List<OrderDetailInfo>> searchByCondition(Pager<List<OrderDetailInfo>> pager, OrderDetailInfoQuery condition)
        {
            pager.data = dao.searchByCondition(pager, condition);
            pager.recTotal = dao.searchCountByCondition(condition);
            return pager;
        }

        public Task<OrderDetailInfo> searchByid(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<int>> update(OrderDetailInfo model)
        {
            throw new NotImplementedException();
        }
    }
}