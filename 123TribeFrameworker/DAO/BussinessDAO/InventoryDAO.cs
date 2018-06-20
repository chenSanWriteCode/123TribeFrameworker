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
            using (LayerDbContext context = new LayerDbContext())
            {
                try
                {
                    context.inventory.Add(t);
                    await context.SaveChangesAsync();
                }
                catch (Exception err)
                {
                    result.addError(err.Message);
                }
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
            result.GroupBy(x => x.materialId);
            result.OrderBy(x => x.materialId);
            result.Skip(start).Take(pager.recPerPage);
            return result.ToList();
        }
        public List<Inventory> searchByCountOrder(Pager<List<Inventory>> pager)
        {
            int start = (pager.page - 1) * pager.recPerPage;
            LayerDbContext context = new LayerDbContext();
            var result = context.inventory.Where(x => x.id > 0);
            result.GroupBy(x => x.materialId);
            result.OrderByDescending(x => x.count);
            result.Skip(start).Take(pager.recPerPage);
            return result.ToList();
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
        public int searchCountByCondition(InventoryQuery t)
        {
            LayerDbContext context = new LayerDbContext();
            var result = context.inventory.Where(x => x.id > 0);
            result = string.IsNullOrEmpty(t.materialName) ? result : result.Where(x => x.materialInfo.materialName.Contains(t.materialName));
            result = string.IsNullOrEmpty(t.mat_size) ? result : result.Where(x => x.materialInfo.mat_size == t.mat_size);
            result = string.IsNullOrEmpty(t.alias) ? result : result.Where(x => x.materialInfo.alias.Contains(t.alias));
            result = string.IsNullOrEmpty(t.remark) ? result : result.Where(x => x.materialInfo.remark.Contains(t.remark));
            result.GroupBy(x => x.materialId);
            return result.Count();
        }

        public Task<Result<int>> update(Inventory t)
        {
            throw new NotImplementedException();
        }
    }
}