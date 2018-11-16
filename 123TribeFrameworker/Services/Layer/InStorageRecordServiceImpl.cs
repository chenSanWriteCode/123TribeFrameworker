using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using _123TribeFrameworker.DAO;
using _123TribeFrameworker.Entity;
using _123TribeFrameworker.Models.BussinessModels;
using _123TribeFrameworker.Models.QueryModel;
using _123TribeFrameworker.Services;
using Unity.Attributes;

namespace _123TribeFrameworker.Services.Layer
{
    public class InStorageRecordServiceImpl : IInStorageRecordService
    {
        [Dependency]
        public IInStorageRecordDAO dao { get; set; }

        public List<InStorageRecordModel> searchAllByCondition(InStorageRecordQuery condition)
        {
            List<InStorageRecordModel> result = new List<InStorageRecordModel>();
            if (condition!=null)
            {
                Task<List<InStorageRecordModel>> task = Task.Factory.StartNew(() => dao.searchAllByCondition(condition));
                result = task.Result;
            }
            return result;
        }
        public async Task<Pager<List<InStorageRecord>>> searchByCondition(Pager<List<InStorageRecord>> pager, InStorageRecordQuery t)
        {
            pager.data = await Task.Factory.StartNew(() => dao.searchByCondition(pager, t));
            pager.recTotal = await Task.Factory.StartNew(() => dao.searchCountByCondition(t));
            return pager;
        }

        public async Task<Pager<List<InStorageRecordModel>>> searchSumByCondition(Pager<List<InStorageRecordModel>> pager, InStorageRecordQuery t)
        {
            pager.data = await Task.Factory.StartNew(() => dao.searchSumByCondition(pager, t));
            pager.recTotal = await Task.Factory.StartNew(() => dao.searchSumCountByCondition(t));
            return pager;
        }
    }
}