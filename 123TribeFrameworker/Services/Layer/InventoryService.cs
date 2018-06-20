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
    public class InventoryService : IInventoryService
    {
        [Dependency]
        public IInventoryDAO dao { get; set; }
        public Pager<List<Inventory>> searchByCondition(Pager<List<Inventory>> pager, InventoryQuery condition)
        {
            pager.data = dao.searchByCondition(pager, condition);
            pager.recTotal = dao.searchCountByCondition(condition);
            return pager;
        }
        /// <summary>
        /// 根据库存数量倒序查询
        /// </summary>
        /// <param name="pager"></param>
        /// <returns></returns>
        public Pager<List<Inventory>> searchByNumOrder(Pager<List<Inventory>> pager)
        {
            InventoryQuery condition = new InventoryQuery();
            pager.data = dao.searchByCountOrder(pager);
            pager.recTotal = dao.searchCountByCondition(condition);
            return pager;
        }
    }
}