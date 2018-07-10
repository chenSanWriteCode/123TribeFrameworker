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
        public List<OutRecordModel> searchByCondition(Pager<List<OutRecordModel>> pager, OutRecordQuery condition)
        {
            int start = (pager.page - 1) * pager.recPerPage;
            LayerDbContext context = new LayerDbContext();
            var result = from t in context.tradingRecord
                         join p in context.profitRecord on new { cashOrder = t.cashOrder, materialId = t.materialId } equals new { cashOrder = p.cashOrder, materialId = p.materialId }
                         select new OutRecordModel
                         {
                             cashOrder=t.cashOrder,
                             materialId=t.materialId,
                             alais=t.materialInfo.alias,
                             count=t.count,
                             createdBy=t.createdBy,
                             materialName=t.materialInfo.materialName,
                             mat_size=t.materialInfo.mat_size,
                             priceIn=p.priceIn,
                             priceOut=p.priceOut,
                             profit=p.profit,
                             unit=t.materialInfo.unit,
                             createdDate=t.createdDate
                            
                         };
            result = string.IsNullOrEmpty(condition.cashOrder) ? result : result.Where(t => t.cashOrder.Contains(condition.cashOrder));
            result = string.IsNullOrEmpty(condition.materialName) ? result : result.Where(x => x.materialName==(condition.materialName));
            result = string.IsNullOrEmpty(condition.mat_size) ? result : result.Where(x => x.mat_size == condition.mat_size);
            result = !condition.createdDateBegin.HasValue ? result : result.Where(x => x.createdDate >= condition.createdDateBegin);
            result = !condition.createdDateEnd.HasValue ? result : result.Where(x => x.createdDate <= condition.createdDateEnd);
            result = result.OrderByDescending(x=>x.createdDate).Skip(start).Take(pager.recPerPage);
            return result.ToList();
                        
        }

        public int searchCountByCondition(OutRecordQuery condition)
        {
            LayerDbContext context = new LayerDbContext();
            var result = context.tradingRecord.Where(x => x.id > 0);
            result = string.IsNullOrEmpty(condition.cashOrder) ? result : result.Where(t => t.cashOrder.Contains(condition.cashOrder));
            result = string.IsNullOrEmpty(condition.materialName) ? result : result.Where(x => x.materialInfo.materialName==(condition.materialName));
            result = string.IsNullOrEmpty(condition.mat_size) ? result : result.Where(x => x.materialInfo.mat_size == condition.mat_size);
            result = !condition.createdDateBegin.HasValue ? result : result.Where(x => x.createdDate >= condition.createdDateBegin);
            result = !condition.createdDateEnd.HasValue ? result : result.Where(x => x.createdDate <= condition.createdDateEnd);
            return result.Count();
        }

        public List<OutRecordModel> searchSumByCondition(Pager<List<OutRecordModel>> pager, OutRecordQuery t)
        {
            throw new NotImplementedException();
        }

        public int searchSumCountByCondition(OutRecordQuery t)
        {
            throw new NotImplementedException();
        }
    }
}