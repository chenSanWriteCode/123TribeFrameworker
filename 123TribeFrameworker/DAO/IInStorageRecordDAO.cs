using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _123TribeFrameworker.Entity;
using _123TribeFrameworker.Models.BussinessModels;
using _123TribeFrameworker.Models.QueryModel;

namespace _123TribeFrameworker.DAO
{
    public interface IInStorageRecordDAO : ICommonDAO<InStorageRecord>
    {
        List<InStorageRecordModel> searchAllByCondition(InStorageRecordQuery condition);

        List<InStorageRecord> searchByCondition(Pager<List<InStorageRecord>> pager, InStorageRecordQuery t);

        int searchCountByCondition(InStorageRecordQuery t);
        List<InStorageRecordModel> searchSumByCondition(Pager<List<InStorageRecordModel>> pager, InStorageRecordQuery t);
        int searchSumCountByCondition(InStorageRecordQuery t);
    }
}
