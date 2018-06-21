using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using _123TribeFrameworker.Entity;
using _123TribeFrameworker.Models.QueryModel;

namespace _123TribeFrameworker.DAO.BussinessDAO
{
    public class InStorageRecordDAO : IInStorageRecordDAO
    {
        public List<InStorageRecord> searchAllByCondition(InStorageRecordQuery condition)
        {
            LayerDbContext context = new LayerDbContext();
            var result = context.inStorageRecord.Where(x => x.id > 0);
            result = string.IsNullOrEmpty(condition.alias) ? result : result.Where(x => x.materialInfo.alias.Contains(condition.alias));
            result = !condition.createdDateBegin.HasValue ? result : result.Where(x => x.createdDate > condition.createdDateBegin);
            result = !condition.createdDateEnd.HasValue ? result : result.Where(x => x.createdDate < condition.createdDateEnd);
            result = string.IsNullOrEmpty(condition.materialName) ? result : result.Where(x => x.materialInfo.materialName.Contains(condition.materialName));
            result = string.IsNullOrEmpty(condition.mat_size) ? result : result.Where(x => x.materialInfo.mat_size == condition.mat_size);
            result = string.IsNullOrEmpty(condition.orderNo) ? result : result.Where(x => x.orderNo.Contains(condition.orderNo));
            result = string.IsNullOrEmpty(condition.receivedOrder) ? result : result.Where(x => x.receivedOrder == condition.receivedOrder);
            return result.ToList();
        }

        public Task<InStorageRecord> searchById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<int>> update(InStorageRecord t)
        {
            throw new NotImplementedException();
        }
        public Task<Result<int>> add(InStorageRecord t)
        {
            throw new NotImplementedException();
        }
        public Task<Result<int>> deleteById(int id)
        {
            throw new NotImplementedException();
        }
    }
}