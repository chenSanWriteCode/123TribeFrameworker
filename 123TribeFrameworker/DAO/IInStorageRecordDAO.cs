using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _123TribeFrameworker.Entity;
using _123TribeFrameworker.Models.QueryModel;

namespace _123TribeFrameworker.DAO
{
    public interface IInStorageRecordDAO : ICommonDAO<InStorageRecord>
    {
        List<InStorageRecord> searchAllByCondition(InStorageRecordQuery condition);
    }
}
