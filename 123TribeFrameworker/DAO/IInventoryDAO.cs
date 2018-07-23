using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _123TribeFrameworker.CommonTools;
using _123TribeFrameworker.Entity;
using _123TribeFrameworker.Models.BussinessModels;
using _123TribeFrameworker.Models.QueryModel;

namespace _123TribeFrameworker.DAO
{
    public interface IInventoryDAO:ICommonDAO<Inventory>
    {
        Task<List<InventorySimpleModel>> searchByCountOrder(Pager<List<InventorySimpleModel>> pager);

        Task<List<InventorySimpleModel>> searchByCondition(Pager<List<InventorySimpleModel>> pager, InventoryQuery t);
        /// <summary>
        /// 物料个数,查询pager的总条目
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        int searchCountByCondition();
        /// <summary>
        /// 通过查询条件查询库存个数
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        float searchInventoryCountByCondition(InventoryQuery t);

        List<InventorySimpleModel> searchTenLackInventory();
        /// <summary>
        /// 柱状图
        /// </summary>
        /// <returns></returns>
        InventoryTool searchTenInventoryCount();
    }
}
