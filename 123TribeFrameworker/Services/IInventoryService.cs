using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _123TribeFrameworker.Entity;
using _123TribeFrameworker.Models.BussinessModels;
using _123TribeFrameworker.Models.QueryModel;

namespace _123TribeFrameworker.Services
{
    public interface IInventoryService
    {
        Task<Pager<List<InventorySimpleModel>>> searchByNumOrder(Pager<List<InventorySimpleModel>> pager);
        Task<Pager<List<InventorySimpleModel>>> searchByCondition(Pager<List<InventorySimpleModel>> pager, InventoryQuery condition);
        /// <summary>
        /// 查询是个库存不足的物料，将其拼接成字符串
        /// </summary>
        /// <returns></returns>
        Task<string> searchTenLackInventory();
    }
}
