using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _123TribeFrameworker.Entity;
using _123TribeFrameworker.Models.QueryModel;

namespace _123TribeFrameworker.DAO
{
    public interface IOrderInfoDAO:ICommonDAO<OrderInfo>
    {
        Task<OrderInfo> searchByOrder(string orderNo);

        List<OrderInfo> searchByCondition(Pager<List<OrderInfo>> pager, OrderInfoQuery t);

        int searchCountByCondition(OrderInfoQuery t);
    }
}
