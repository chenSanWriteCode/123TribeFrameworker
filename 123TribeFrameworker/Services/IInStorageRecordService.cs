using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _123TribeFrameworker.Entity;
using _123TribeFrameworker.Models.BussinessModels;
using _123TribeFrameworker.Models.QueryModel;

namespace _123TribeFrameworker.Services
{
    public interface IInStorageRecordService
    {
        List<InStorageRecordModel> searchAllByCondition(InStorageRecordQuery condition);
        Task<Pager<List<InStorageRecord>>> searchByCondition(Pager<List<InStorageRecord>> pager, InStorageRecordQuery t);
        Task<Pager<List<InStorageRecordModel>>> searchSumByCondition(Pager<List<InStorageRecordModel>> pager, InStorageRecordQuery condition);
    }
}
