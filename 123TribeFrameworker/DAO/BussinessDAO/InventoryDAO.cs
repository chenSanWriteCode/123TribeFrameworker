using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using _123TribeFrameworker.Entity;
using _123TribeFrameworker.Models.QueryModel;

namespace _123TribeFrameworker.DAO.BussinessDAO
{
    public class InventoryDAO : IInventoryDAO
    {
        /// <summary>
        /// 增加单个物料库存
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public async Task<Result<int>> add(Inventory t)
        {
            Result<int> result = new Result<int>();
            try
            {
                using (LayerDbContext context = new LayerDbContext())
                {
                    context.inventory.Add(t);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception err)
            {
                result.addError(err.Message);
            }
            return result;
        }
        public Task<Result<int>> deleteById(int id)
        {
            return null;
        }
        /// <summary>
        /// 查询页面展示
        /// </summary>
        /// <param name="pager"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public List<Inventory> searchByCondition(Pager<List<Inventory>> pager, InventoryQuery t)
        {
            int start = (pager.page - 1) * pager.recPerPage;
            LayerDbContext context = new LayerDbContext();
            var result = context.inventory.Where(x => x.id > 0);
            result = string.IsNullOrEmpty(t.materialName) ? result : result.Where(x => x.materialInfo.materialName.Contains(t.materialName));
            result = string.IsNullOrEmpty(t.mat_size) ? result : result.Where(x => x.materialInfo.mat_size == t.mat_size);
            result = string.IsNullOrEmpty(t.alias) ? result : result.Where(x => x.materialInfo.alias.Contains(t.alias));
            result = string.IsNullOrEmpty(t.remark) ? result : result.Where(x => x.materialInfo.remark.Contains(t.remark));
            result.GroupBy(x => x.materialId).Select(x=>new { materialId=x.Key, count=x.Sum(item=>item.count) });
            result.OrderBy(x => x.materialId);
            var materialInfoList = context.materialInfos.Where(x => true);
            var part = from x in materialInfoList where (result.Select(y => y.materialId).Contains(x.id)) select x;
            var partList = part.ToList();
            var list1 = from x in result
                        join y in part on x.materialId equals y.id
                        select new Inventory
                        {

                        };
            result.Skip(start).Take(pager.recPerPage);
            return result.ToList();
        }
        
        public int searchCountByCondition(InventoryQuery t)
        {
            LayerDbContext context = new LayerDbContext();
            var result = context.inventory.Where(x => x.id > 0);
            result = string.IsNullOrEmpty(t.materialName) ? result : result.Where(x => x.materialInfo.materialName.Contains(t.materialName));
            result = string.IsNullOrEmpty(t.mat_size) ? result : result.Where(x => x.materialInfo.mat_size == t.mat_size);
            result = string.IsNullOrEmpty(t.alias) ? result : result.Where(x => x.materialInfo.alias.Contains(t.alias));
            result = string.IsNullOrEmpty(t.remark) ? result : result.Where(x => x.materialInfo.remark.Contains(t.remark));
            result.GroupBy(x => x.materialId).Select(x => new { materialId = x.Key });
            return result.Count();
        }
        /// <summary>
        /// 根据库存量倒序查询
        /// </summary>
        /// <param name="pager"></param>
        /// <returns></returns>
        public List<Inventory> searchByCountOrder(Pager<List<Inventory>> pager)
        {
            int start = (pager.page - 1) * pager.recPerPage;
            LayerDbContext context = new LayerDbContext();
            var result = context.inventory.Where(x => x.id > 0).GroupBy(x => x.materialId).Select(x => new { materialId = x.Key, count = x.Sum(item => item.count) }).OrderByDescending(x => x.count).Skip(start).Take(pager.recPerPage).ToList();
            List<Inventory> list = new List<Inventory>();
            Inventory model = null;
            foreach (var item in result)
            {
                model = new Inventory();
                model.materialId = item.materialId;
                model.count = item.count;
                list.Add(model);
            }
            return list;
        }
        
        public Task<Inventory> searchById(int id)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 总数
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        

        public Task<Result<int>> update(Inventory t)
        {
            throw new NotImplementedException();
        }
    }
}