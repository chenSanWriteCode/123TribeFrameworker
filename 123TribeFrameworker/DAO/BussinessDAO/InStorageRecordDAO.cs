using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using _123TribeFrameworker.Entity;
using _123TribeFrameworker.Models.BussinessModels;
using _123TribeFrameworker.Models.QueryModel;

namespace _123TribeFrameworker.DAO.BussinessDAO
{
    public class InStorageRecordDAO : IInStorageRecordDAO
    {
        /// <summary>
        /// 查询所有入库记录汇总  根据条件  
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public List<InStorageRecordModel> searchAllByCondition(InStorageRecordQuery condition)
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
            var inStorages = result.GroupBy(x => new { x.materialId, x.orderNo }).Select(x => new
            {
                materialId = x.Key.materialId,
                orderNo = x.Key.orderNo,
                countReal = x.Sum(item => item.countReal),
                priceIn = x.Max(item => item.priceIn),
                receivedOrder = x.Max(item => item.receivedOrder)
            });
            var orderDetailInfos = context.orderDetailInfo;
            var returnResult = from x in inStorages
                               join y in orderDetailInfos on new { x.orderNo, x.materialId } equals new { y.orderNo, y.materialId }
                               select new InStorageRecordModel
                               {
                                   materialId = x.materialId,
                                   orderNo = x.orderNo,
                                   countReal = x.countReal,
                                   priceIn = x.priceIn,
                                   receivedOrder = x.receivedOrder,
                                   countReference = y.num,
                                   materialName = y.materialName,
                                   mat_size = y.materialInfo.mat_size

                               };
            return returnResult.ToList();
        }

       
        /// <summary>
        /// 查询所有入库记录  根据条件 pager
        /// </summary>
        /// <param name="pager"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public List<InStorageRecord> searchByCondition(Pager<List<InStorageRecord>> pager, InStorageRecordQuery condition)
        {
            int start = (pager.page - 1) * pager.recPerPage;
            LayerDbContext context = new LayerDbContext();
            var result = context.inStorageRecord.Where(x => x.id > 0);
            result = string.IsNullOrEmpty(condition.alias) ? result : result.Where(x => x.materialInfo.alias.Contains(condition.alias));
            result = !condition.createdDateBegin.HasValue ? result : result.Where(x => x.createdDate > condition.createdDateBegin);
            result = !condition.createdDateEnd.HasValue ? result : result.Where(x => x.createdDate < condition.createdDateEnd);
            result = string.IsNullOrEmpty(condition.materialName) ? result : result.Where(x => x.materialInfo.materialName.Contains(condition.materialName));
            result = string.IsNullOrEmpty(condition.mat_size) ? result : result.Where(x => x.materialInfo.mat_size == condition.mat_size);
            result = string.IsNullOrEmpty(condition.orderNo) ? result : result.Where(x => x.orderNo.Contains(condition.orderNo));
            result = string.IsNullOrEmpty(condition.receivedOrder) ? result : result.Where(x => x.receivedOrder == condition.receivedOrder);
            result = result.OrderBy(x => x.materialInfo.materialName);
            return result.Skip(start).Take(pager.recPerPage).ToList();
        }

        /// <summary>
        /// 查询所有入库记录条数 根据条件 
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public int searchCountByCondition(InStorageRecordQuery condition)
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
            return result.Count();
        }

        /// <summary>
        /// 查询所有入库记录汇总  根据条件  pager
        /// </summary>
        /// <param name="pager"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public List<InStorageRecordModel> searchSumByCondition(Pager<List<InStorageRecordModel>> pager, InStorageRecordQuery condition)
        {
            int start = (pager.page - 1) * pager.recPerPage;
            LayerDbContext context = new LayerDbContext();
            var result = context.inStorageRecord.Where(x => x.id > 0);
            result = string.IsNullOrEmpty(condition.alias) ? result : result.Where(x => x.materialInfo.alias.Contains(condition.alias));
            result = !condition.createdDateBegin.HasValue ? result : result.Where(x => x.createdDate > condition.createdDateBegin);
            result = !condition.createdDateEnd.HasValue ? result : result.Where(x => x.createdDate < condition.createdDateEnd);
            result = string.IsNullOrEmpty(condition.materialName) ? result : result.Where(x => x.materialInfo.materialName.Contains(condition.materialName));
            result = string.IsNullOrEmpty(condition.mat_size) ? result : result.Where(x => x.materialInfo.mat_size == condition.mat_size);
            result = string.IsNullOrEmpty(condition.orderNo) ? result : result.Where(x => x.orderNo.Contains(condition.orderNo));
            result = string.IsNullOrEmpty(condition.receivedOrder) ? result : result.Where(x => x.receivedOrder == condition.receivedOrder);
            var inStorages = result.GroupBy(x => new { x.materialId, x.orderNo }).Select(x => new
            {
                materialId = x.Key.materialId,
                orderNo = x.Key.orderNo,
                countReal = x.Sum(item => item.countReal),
                priceIn = x.Max(item => item.priceIn),
                receivedOrder = x.Max(item => item.receivedOrder)
            });
            var orderDetailInfos = context.orderDetailInfo;
            var returnResult = from x in inStorages
                               join y in orderDetailInfos on new { x.orderNo, x.materialId } equals new { y.orderNo, y.materialId }
                               select new InStorageRecordModel
                               {
                                   materialId = x.materialId,
                                   orderNo = x.orderNo,
                                   countReal = x.countReal,
                                   priceIn = x.priceIn,
                                   receivedOrder = x.receivedOrder,
                                   countReference = y.num,
                                   materialName = y.materialName,
                                   mat_size = y.materialInfo.mat_size

                               };
            return returnResult.OrderBy(x => x.materialName).Skip(start).Take(pager.recPerPage).ToList();
        }
        /// <summary>
        /// 查询所有入库记录汇总条数 根据条件
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public int searchSumCountByCondition(InStorageRecordQuery condition)
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
            var inStorages = result.GroupBy(x => new { x.materialId, x.orderNo }).Select(x => new
            {
                materialId = x.Key.materialId,
            });
            return inStorages.Count();
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