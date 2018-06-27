using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _123TribeFrameworker.Entity;
using _123TribeFrameworker.Models.BussinessModels;
using _123TribeFrameworker.Models.QueryModel;

namespace _123TribeFrameworker.DAO
{
    public interface IInventoryDAO:ICommonDAO<Inventory>
    {
        Task<List<InventorySimpleModel>> searchByCountOrder(Pager<List<InventorySimpleModel>> pager);

        Task<List<InventorySimpleModel>> searchByCondition(Pager<List<InventorySimpleModel>> pager, InventoryQuery t);

        int searchCountByCondition(InventoryQuery t);
    }
}
