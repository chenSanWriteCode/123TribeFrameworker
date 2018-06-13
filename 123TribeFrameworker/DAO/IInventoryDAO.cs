using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _123TribeFrameworker.Entity;
using _123TribeFrameworker.Models.QueryModel;

namespace _123TribeFrameworker.DAO
{
    public interface IInventoryDAO:ICommonDAO<Inventory>
    {
        Task<Result<int>> addRange(List<Inventory> list);

        List<Inventory> searchByCountOrder(Pager<List<Inventory>> pager);

        List<Inventory> searchByCondition(Pager<List<Inventory>> pager, InventoryQuery t);

        int searchCountByCondition(InventoryQuery t);
    }
}
