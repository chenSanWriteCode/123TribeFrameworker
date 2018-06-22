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
        public async Task<Pager<List<InventorySimpleModel>>> searchByNumOrder(Pager<List<InventorySimpleModel>> pager)
        {
            InventoryQuery condition = new InventoryQuery();
            Pager<List<Inventory>> pagerCopy = new Pager<List<Inventory>>();
            pagerCopy.page = pager.page;
            pagerCopy.recPerPage = pager.recPerPage;
            var fackData = dao.searchByCountOrder(pagerCopy);
            var materialData = await materialDao.searchByIds(fackData.Select(x => x.materialId).ToArray());
            var returnData = from x in fackData
                             join y in materialData on x.materialId equals y.id
                             select new InventorySimpleModel
                             {
                                 materialId=x.materialId,
                                 count=x.count,
                                 materialName=y.materialName,
                                 mat_size=y.mat_size
                             };
            pager.data = returnData.ToList();
            pager.recTotal = dao.searchCountByCondition(condition);
            return pager;
        }
    }
}