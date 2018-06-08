using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _123TribeFrameworker.Entity;
using _123TribeFrameworker.Models.QueryModel;

namespace _123TribeFrameworker.Services
{
    public interface IOrderInfoService:ICommonService<OrderInfo>
    {
        Task<OrderInfo> searchByOrder(string orderNo);
        Pager<List<OrderInfo>> searchByCondition(Pager<List<OrderInfo>> pager, OrderInfoQuery condition);
    }
}
