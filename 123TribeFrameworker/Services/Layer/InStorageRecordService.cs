using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using _123TribeFrameworker.DAO;
using _123TribeFrameworker.Entity;
using _123TribeFrameworker.Models.QueryModel;
using _123TribeFrameworker.Services;
using Unity.Attributes;

namespace _123TribeFrameworker.Services.Layer
{
    public class InStorageRecordService : IInStorageRecordService
    {
        [Dependency]
        public IInStorageRecordDAO dao { get; set; }

        public List<InStorageRecord> searchAllByCondition(InStorageRecordQuery condition)
        {
            List<InStorageRecord> result = new List<InStorageRecord>();
            if (condition!=null)
            {
                Task<List<InStorageRecord>> task = Task.Factory.StartNew(() => dao.searchAllByCondition(condition));
                result = task.Result;
            }
            return result;
        }
    }
}