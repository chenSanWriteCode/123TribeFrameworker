using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _123TribeFrameworker.Entity;
using _123TribeFrameworker.Models.QueryModel;

namespace _123TribeFrameworker.DAO
{
    public interface IOrderDetailInfoDAO : ICommonDAO<OrderDetailInfo>
    {
        Task<int> addRange(List<OrderDetailInfo> list);

        List<OrderDetailInfo> searchByCondition(Pager<List<OrderDetailInfo>> pager, OrderDetailInfoQuery t);

        int searchCountByCondition(OrderDetailInfoQuery t);
    }
}
