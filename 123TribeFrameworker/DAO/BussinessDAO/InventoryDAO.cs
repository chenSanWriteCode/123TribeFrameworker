﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using _123TribeFrameworker.Entity;
using _123TribeFrameworker.Models.BussinessModels;
using _123TribeFrameworker.Models.QueryModel;
using Unity.Attributes;

namespace _123TribeFrameworker.DAO.BussinessDAO
{
    public class InventoryDAO : IInventoryDAO
    {
        [Dependency]
        public IMaterialInfoDAO materialDao { get; set; } 
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
        public async Task<List<InventorySimpleModel>> searchByCondition(Pager<List<InventorySimpleModel>> pager, InventoryQuery t)
        {
            int start = (pager.page - 1) * pager.recPerPage;
            LayerDbContext context = new LayerDbContext();
            var inventoryResult = context.inventory.GroupBy(x => x.materialId).Select(x => new { materialId = x.Key, count = x.Sum(item => item.count) });

            var materialResult = context.materialInfos.Where(x=>x.id>0);
            materialResult = string.IsNullOrEmpty(t.materialName) ? materialResult : materialResult.Where(x => x.materialName.Contains(t.materialName));
            materialResult = string.IsNullOrEmpty(t.mat_size) ? materialResult : materialResult.Where(x => x.mat_size == t.mat_size);
            materialResult = string.IsNullOrEmpty(t.alias) ? materialResult : materialResult.Where(x => x.alias.Contains(t.alias));
            materialResult = string.IsNullOrEmpty(t.remark) ? materialResult : materialResult.Where(x => x.remark.Contains(t.remark));

            var returnData = from x in materialResult
                             join y in inventoryResult on x.id equals y.materialId
                             into Temp from temp in Temp.DefaultIfEmpty()
                             select new InventorySimpleModel
                             {
                                 materialId = x.id,
                                 alais = x.alias,
                                 materialName = x.materialName,
                                 mat_size = x.mat_size,
                                 unit = x.unit,
                                 alarmCount = x.alarmCount,
                                 count = temp==null?0:temp.count
                             };
            returnData = returnData.OrderBy(x => x.count).Skip(start).Take(pager.recPerPage);
            return await Task.Factory.StartNew(() => returnData.ToList());
        }

        public int searchCountByCondition(InventoryQuery t)
        {
            LayerDbContext context = new LayerDbContext();
            //var result = context.inventory.Where(x => x.id > 0);
            //result = string.IsNullOrEmpty(t.materialName) ? result : result.Where(x => x.materialInfo.materialName.Contains(t.materialName));
            //result = string.IsNullOrEmpty(t.mat_size) ? result : result.Where(x => x.materialInfo.mat_size == t.mat_size);
            //result = string.IsNullOrEmpty(t.alias) ? result : result.Where(x => x.materialInfo.alias.Contains(t.alias));
            //result = string.IsNullOrEmpty(t.remark) ? result : result.Where(x => x.materialInfo.remark.Contains(t.remark));
            //result.GroupBy(x => x.materialId).Select(x => new { materialId = x.Key });
            return context.materialInfos.Count();
        }
        /// <summary>
        /// 根据库存量倒序查询
        /// </summary>
        /// <param name="pager"></param>
        /// <returns></returns>
        public async Task<List<InventorySimpleModel>> searchByCountOrder(Pager<List<InventorySimpleModel>> pager)
        {
            int start = (pager.page - 1) * pager.recPerPage;
            LayerDbContext context = new LayerDbContext();
            var inventoryResult = context.inventory.GroupBy(x => x.materialId).Select(x => new { materialId = x.Key, count = x.Sum(item => item.count) });

            var materialResult = context.materialInfos;

            var returnData = from x in materialResult
                             join y in inventoryResult on x.id equals y.materialId
                             into Temp
                             from temp in Temp.DefaultIfEmpty()
                             select new InventorySimpleModel
                             {
                                 materialId = x.id,
                                 alais = x.alias,
                                 materialName = x.materialName,
                                 mat_size = x.mat_size,
                                 unit = x.unit,
                                 alarmCount = x.alarmCount,
                                 count = temp == null ? 0 : temp.count
                             };
            returnData = returnData.OrderBy(x => x.count).Skip(start).Take(pager.recPerPage);
            return await Task.Factory.StartNew(() => returnData.ToList());
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