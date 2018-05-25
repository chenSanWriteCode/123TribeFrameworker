using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _123TribeFrameworker.Entity;

namespace _123TribeFrameworker.DAO
{
    public interface IOrderInfoDAO:ICommonDAO<OrderInfo>
    {
        Task<OrderInfo> searchByOrder(string orderNo);
    }
}
