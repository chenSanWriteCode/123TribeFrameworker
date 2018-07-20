using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _123TribeFrameworker.Entity;
using _123TribeFrameworker.Models.BussinessModels;
using _123TribeFrameworker.Models.QueryModel;

namespace _123TribeFrameworker.DAO.BussinessDAO
{
    public class OutRecordDAO : IOutRecordDAO
    {
        /// <summary>
        /// 出库查询
        /// </summary>
        /// <param name="pager"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public List<OutRecordModel> searchByCondition(Pager<List<OutRecordModel>> pager, OutRecordQuery condition)
        {
            int start = (pager.page - 1) * pager.recPerPage;
            LayerDbContext context = new LayerDbContext();
            var result = from t in context.tradingRecord
                         join p in context.profitRecord on new { cashOrder = t.cashOrder, materialId = t.materialId } equals new { cashOrder = p.cashOrder, materialId = p.materialId }
                         select new OutRecordModel
                         {
                             cashOrder = t.cashOrder,
                             materialId = t.materialId,
                             alais = t.materialInfo.alias,
                             count = t.count,
                             createdBy = t.createdBy,
                             materialName = t.materialInfo.materialName,
                             mat_size = t.materialInfo.mat_size,
                             priceIn = p.priceIn,
                             priceOut = p.priceOut,
                             profit = p.profit,
                             unit = t.materialInfo.unit,
                             createdDate = t.createdDate

                         };
            result = string.IsNullOrEmpty(condition.cashOrder) ? result : result.Where(t => t.cashOrder.Contains(condition.cashOrder));
            result = string.IsNullOrEmpty(condition.materialName) ? result : result.Where(x => x.materialName == (condition.materialName));
            result = string.IsNullOrEmpty(condition.mat_size) ? result : result.Where(x => x.mat_size == condition.mat_size);
            result = !condition.createdDateBegin.HasValue ? result : result.Where(x => x.createdDate >= condition.createdDateBegin);
            result = !condition.createdDateEnd.HasValue ? result : result.Where(x => x.createdDate <= condition.createdDateEnd);
            result = result.OrderByDescending(x => x.createdDate).Skip(start).Take(pager.recPerPage);
            return result.ToList();

        }

        public int searchCountByCondition(OutRecordQuery condition)
        {
            LayerDbContext context = new LayerDbContext();
            var result = context.tradingRecord.Where(x => x.id > 0);
            result = string.IsNullOrEmpty(condition.cashOrder) ? result : result.Where(t => t.cashOrder.Contains(condition.cashOrder));
            result = string.IsNullOrEmpty(condition.materialName) ? result : result.Where(x => x.materialInfo.materialName == (condition.materialName));
            result = string.IsNullOrEmpty(condition.mat_size) ? result : result.Where(x => x.materialInfo.mat_size == condition.mat_size);
            result = !condition.createdDateBegin.HasValue ? result : result.Where(x => x.createdDate >= condition.createdDateBegin);
            result = !condition.createdDateEnd.HasValue ? result : result.Where(x => x.createdDate <= condition.createdDateEnd);
            return result.Count();
        }
        public float searchSumProfitByCondition(OutRecordQuery condition)
        {
            LayerDbContext context = new LayerDbContext();
            var result = context.tradingRecord.Where(x=>x.id>0);
            result = string.IsNullOrEmpty(condition.cashOrder) ? result : result.Where(t => t.cashOrder.Contains(condition.cashOrder));
            result = string.IsNullOrEmpty(condition.materialName) ? result : result.Where(x => x.materialInfo.materialName == (condition.materialName));
            result = string.IsNullOrEmpty(condition.mat_size) ? result : result.Where(x => x.materialInfo.mat_size == condition.mat_size);
            result = !condition.createdDateBegin.HasValue ? result : result.Where(x => x.createdDate >= condition.createdDateBegin);
            result = !condition.createdDateEnd.HasValue ? result : result.Where(x => x.createdDate <= condition.createdDateEnd);
            var proResult = from t in result
                         join p in context.profitRecord on new { cashOrder = t.cashOrder, materialId = t.materialId } equals new { cashOrder = p.cashOrder, materialId = p.materialId }
                         select p.profit;
            if (proResult.Count()==0)
            {
                return 0;
            }
            else
            {
                return proResult.Sum();
            }
        }
        /// <summary>
        /// 热销前十查询（根据频率）
        /// </summary>
        /// <returns></returns>
        public List<MaterialInfo> searchHotTen()
        {
            List<MaterialInfo> result = new List<MaterialInfo>();
            LayerDbContext context = new LayerDbContext();
            var hotId = context.tradingRecord.GroupBy(x => x.materialId).Select(x => new { materialId = x.Key, count = x.Count(),sumNum=x.Sum(item=>item.count) }).OrderByDescending(x => x.count).ThenByDescending(x=>x.sumNum).Take(9).ToList();
            result = (from x in hotId join y in context.materialInfos on x.materialId equals y.id select y).ToList();
            return result;
        }
        /// <summary>
        /// 出库汇总查询
        /// </summary>
        /// <param name="pager"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public List<OutRecordModel> searchSumByCondition(Pager<List<OutRecordModel>> pager, OutRecordQuery condition)
        {
            int start = (pager.page - 1) * pager.recPerPage;
            LayerDbContext context = new LayerDbContext();
            var result = from t in context.tradingRecord
                         join p in context.profitRecord on new { cashOrder = t.cashOrder, materialId = t.materialId } equals new { cashOrder = p.cashOrder, materialId = p.materialId }
                         select new OutRecordModel
                         {
                             cashOrder = t.cashOrder,
                             materialId = t.materialId,
                             alais = t.materialInfo.alias,
                             count = t.count,
                             createdBy = t.createdBy,
                             materialName = t.materialInfo.materialName,
                             mat_size = t.materialInfo.mat_size,
                             priceIn = p.priceIn,
                             priceOut = p.priceOut,
                             profit = p.profit,
                             unit = t.materialInfo.unit,
                             createdDate = t.createdDate

                         };
            result = string.IsNullOrEmpty(condition.cashOrder) ? result : result.Where(t => t.cashOrder.Contains(condition.cashOrder));
            result = string.IsNullOrEmpty(condition.materialName) ? result : result.Where(x => x.materialName == (condition.materialName));
            result = string.IsNullOrEmpty(condition.mat_size) ? result : result.Where(x => x.mat_size == condition.mat_size);
            result = !condition.createdDateBegin.HasValue ? result : result.Where(x => x.createdDate >= condition.createdDateBegin);
            result = !condition.createdDateEnd.HasValue ? result : result.Where(x => x.createdDate <= condition.createdDateEnd);

            var result1 = result.GroupBy(x => x.materialId).Select(x => new
            {
                materialId = x.Key,
                materialName = x.Max(item => item.materialName),
                mat_size = x.Max(item => item.mat_size),
                count = x.Sum(item => item.count),
                profit = x.Sum(item => item.profit)
            }).OrderBy(x=>x.count).Skip(start).Take(pager.recPerPage).ToList();
            List<OutRecordModel> returnResult = new List<OutRecordModel>();
            OutRecordModel model = null;
            foreach (var item in result1)
            {
                model = new OutRecordModel();
                model.materialId = item.materialId;
                model.materialName = item.materialName;
                model.mat_size = item.mat_size;
                model.count = item.count;
                model.profit = item.profit;
                returnResult.Add(model);
            }
            return returnResult;
        }

        public int searchSumCountByCondition(OutRecordQuery condition)
        {
            LayerDbContext context = new LayerDbContext();
            var result = from t in context.tradingRecord
                         join p in context.profitRecord on new { cashOrder = t.cashOrder, materialId = t.materialId } equals new { cashOrder = p.cashOrder, materialId = p.materialId }
                         select new OutRecordModel
                         {
                             cashOrder = t.cashOrder,
                             materialId = t.materialId,
                             alais = t.materialInfo.alias,
                             count = t.count,
                             createdBy = t.createdBy,
                             materialName = t.materialInfo.materialName,
                             mat_size = t.materialInfo.mat_size,
                             priceIn = p.priceIn,
                             priceOut = p.priceOut,
                             profit = p.profit,
                             unit = t.materialInfo.unit,
                             createdDate = t.createdDate

                         };
            result = string.IsNullOrEmpty(condition.cashOrder) ? result : result.Where(t => t.cashOrder.Contains(condition.cashOrder));
            result = string.IsNullOrEmpty(condition.materialName) ? result : result.Where(x => x.materialName == (condition.materialName));
            result = string.IsNullOrEmpty(condition.mat_size) ? result : result.Where(x => x.mat_size == condition.mat_size);
            result = !condition.createdDateBegin.HasValue ? result : result.Where(x => x.createdDate >= condition.createdDateBegin);
            result = !condition.createdDateEnd.HasValue ? result : result.Where(x => x.createdDate <= condition.createdDateEnd);

            var result1 = result.GroupBy(x => x.materialId).Select(x => new
            {
                materialId = x.Key,
                materialName = x.Max(item => item.materialName),
                mat_size = x.Max(item => item.mat_size),
                count = x.Sum(item => item.count),
                profit = x.Sum(item => item.profit)
            }).Count();
            return result1;
        }

       
    }
}