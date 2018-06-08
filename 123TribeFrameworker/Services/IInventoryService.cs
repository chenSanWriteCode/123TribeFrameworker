using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _123TribeFrameworker.Entity;
using _123TribeFrameworker.Models.QueryModel;

namespace _123TribeFrameworker.Services
{
    public interface IInventoryService:ICommonService<Inventory>
    {
        Pager<List<Inventory>> searchByNumOrder(Pager<List<Inventory>> pager);
        Pager<List<Inventory>> searchByCondition(Pager<List<Inventory>> pager, InventoryQuery condition);
    }
}
