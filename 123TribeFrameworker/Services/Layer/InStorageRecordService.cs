using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using _123TribeFrameworker.DAO;
using _123TribeFrameworker.Entity;
using _123TribeFrameworker.Services;
using Unity.Attributes;

namespace _123TribeFrameworker.Services.Layer
{
    public class InStorageRecordService : IInStorageRecordService
    {
        [Dependency]
        public IInStorageRecordDAO dao { get; set; }
        [Dependency]
        public IInventoryDAO inventoryDao { get; set; }
        
        /// <summary>
        /// 增加入库记录
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public async Task<Result<int>> addRangeAsync(List<InStorageRecord> list)
        {
            Result<int> result = new Result<int>();
            //增加入库记录
            result = await dao.addRange(list);
            if (result.result)
            {
                List<Inventory> inventoryList = new List<Inventory>();
                Inventory model = null;
                foreach (var item in list)
                {
                    model = new Inventory()
                    {
                        count = item.countReal,
                        materialId = item.materialId,
                        priceIn = item.priceIn
                    };
                    inventoryList.Add(model);
                }
                //增加库存
                result = await inventoryDao.addRange(inventoryList);
            }
            return result;
        }
    }
}