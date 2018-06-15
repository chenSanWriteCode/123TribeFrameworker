﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using _123TribeFrameworker.DAO.DirDAO;
using _123TribeFrameworker.Entity;
using _123TribeFrameworker.Models.QueryModel;

namespace _123TribeFrameworker.DAO.BussinessDAO
{
    public class OrderDetailInfoDAO : IOrderDetailInfoDAO
    {
        public async Task<int> addRange(List<OrderDetailInfo> list)
        {
            LayerDbContext context = new LayerDbContext();
            context.orderDetailInfo.AddRange(list);
            return await context.SaveChangesAsync();
        }
        /// <summary>
        /// 查询所有
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public List<OrderDetailInfo> searchAllByCondition(OrderDetailInfoQuery t)
        {
            LayerDbContext context = new LayerDbContext();
            var result = context.orderDetailInfo.Where(x => x.id > 0);
            result = string.IsNullOrEmpty(t.orderNo) ? result : result.Where(x => x.orderNo.Contains(t.orderNo));
            result = string.IsNullOrEmpty(t.materialName) ? result : result.Where(x => x.materialInfo.materialName.Contains(t.materialName));
            result = string.IsNullOrEmpty(t.mat_size) ? result : result.Where(x => x.materialInfo.mat_size == t.mat_size);
            result = string.IsNullOrEmpty(t.alias) ? result : result.Where(x => x.materialInfo.alias.Contains(t.alias));
            result = string.IsNullOrEmpty(t.remark) ? result : result.Where(x => x.materialInfo.remark.Contains(t.remark));
            result = string.IsNullOrEmpty(t.status) ? result : result.Where(x => x.orderInfo.status == t.status);
            result = !t.createdDateBegin.HasValue ? result : result.Where(x => x.createdDate >= t.createdDateBegin);
            result = !t.createdDateEnd.HasValue ? result : result.Where(X => X.createdDate <= t.createdDateEnd);
            result = !t.receivedDateBegin.HasValue ? result : result.Where(x => x.orderInfo.receivedDate >= t.receivedDateBegin);
            result = !t.receivedDateEnd.HasValue ? result : result.Where(x => x.orderInfo.receivedDateEnd <= t.receivedDateEnd);
            return result.ToList();
        }
        /// <summary>
        /// 根据pager
        /// </summary>
        /// <param name="pager"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public List<OrderDetailInfo> searchByCondition(Pager<List<OrderDetailInfo>> pager, OrderDetailInfoQuery t)
        {
            LayerDbContext context = new LayerDbContext();
            int start = (pager.page - 1) * pager.recPerPage;
            var result = context.orderDetailInfo.Where(x => x.id > 0);
            result = string.IsNullOrEmpty(t.orderNo) ? result : result.Where(x => x.orderNo.Contains(t.orderNo));
            result = string.IsNullOrEmpty(t.materialName) ? result : result.Where(x => x.materialInfo.materialName.Contains(t.materialName));
            result = string.IsNullOrEmpty(t.mat_size) ? result : result.Where(x => x.materialInfo.mat_size == t.mat_size);
            result = string.IsNullOrEmpty(t.alias) ? result : result.Where(x => x.materialInfo.alias.Contains(t.alias));
            result = string.IsNullOrEmpty(t.remark) ? result : result.Where(x => x.materialInfo.remark.Contains(t.remark));
            result = string.IsNullOrEmpty(t.status) ? result : result.Where(x => x.orderInfo.status == t.status);
            result = !t.createdDateBegin.HasValue ? result : result.Where(x => x.createdDate >= t.createdDateBegin);
            result = !t.createdDateEnd.HasValue ? result : result.Where(X => X.createdDate <= t.createdDateEnd);
            result = !t.receivedDateBegin.HasValue ? result : result.Where(x => x.orderInfo.receivedDate >= t.receivedDateBegin);
            result = !t.receivedDateEnd.HasValue ? result : result.Where(x => x.orderInfo.receivedDateEnd <= t.receivedDateEnd);
            result = result.OrderBy(x => x.orderNo);
            result = result.Skip(start).Take(pager.recPerPage);
            return result.ToList();
        }

        public Task<OrderDetailInfo> searchById(int id)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 查询数量
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public int searchCountByCondition(OrderDetailInfoQuery t)
        {
            LayerDbContext context = new LayerDbContext();
            var result = context.orderDetailInfo.Where(x => x.id > 0);
            result = string.IsNullOrEmpty(t.orderNo) ? result : result.Where(x => x.orderNo.Contains(t.orderNo));
            result = string.IsNullOrEmpty(t.materialName) ? result : result.Where(x => x.materialName.Contains(t.materialName));
            result = string.IsNullOrEmpty(t.mat_size) ? result : result.Where(x => x.materialInfo.mat_size == t.mat_size);
            result = string.IsNullOrEmpty(t.alias) ? result : result.Where(x => x.materialInfo.alias.Contains(t.alias));
            result = string.IsNullOrEmpty(t.remark) ? result : result.Where(x => x.materialInfo.remark.Contains(t.remark));
            result = string.IsNullOrEmpty(t.status) ? result : result.Where(x => x.orderInfo.status == t.status);
            result = !t.createdDateBegin.HasValue ? result : result.Where(x => x.createdDate >= t.createdDateBegin);
            result = !t.createdDateEnd.HasValue ? result : result.Where(X => X.createdDate <= t.createdDateEnd);
            result = !t.receivedDateBegin.HasValue ? result : result.Where(x => x.orderInfo.receivedDate >= t.receivedDateBegin);
            result = !t.receivedDateEnd.HasValue ? result : result.Where(x => x.orderInfo.receivedDateEnd <= t.receivedDateEnd);
            return result.Count();
        }


        public Task<Result<int>> add(OrderDetailInfo t)
        {
            throw new NotImplementedException();
        }
        public Task<Result<int>> deleteById(int id)
        {
            throw new NotImplementedException();
        }
        public Task<Result<int>> update(OrderDetailInfo t)
        {
            throw new NotImplementedException();
        }
    }
}