using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using _123TribeFrameworker.DAO;
using _123TribeFrameworker.Entity;
using _123TribeFrameworker.Models.BussinessModels;
using _123TribeFrameworker.Models.QueryModel;
using Unity.Attributes;

namespace _123TribeFrameworker.Services.Layer
{
    public class InventoryService : IInventoryService
    {
        [Dependency]
        public IInventoryDAO dao { get; set; }
        [Dependency]
        public IMaterialInfoDAO materialDao { get; set; }
        public async Task<Pager<List<InventorySimpleModel>>> searchByCondition(Pager<List<InventorySimpleModel>> pager, InventoryQuery condition)
        {
            pager.data = await dao.searchByCondition(pager, condition);
            pager.recTotal = dao.searchCountByCondition();
            return pager;
        }
        /// <summary>
        /// 根据库存数量倒序查询
        /// </summary>
        /// <param name="pager"></param>
        /// <returns></returns>
        public async Task<Pager<List<InventorySimpleModel>>> searchByNumOrder(Pager<List<InventorySimpleModel>> pager)
        {
            pager.data = await dao.searchByCountOrder(pager);
            pager.recTotal = dao.searchCountByCondition();
            return pager;
        }
    }
}