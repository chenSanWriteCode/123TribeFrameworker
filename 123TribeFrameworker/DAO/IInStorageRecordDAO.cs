using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _123TribeFrameworker.Entity;

namespace _123TribeFrameworker.DAO
{
    public interface IInStorageRecordDAO:ICommonDAO<InStorageRecord>
    {
        /// <summary>
        /// 增加多行
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        Task<Result<int>> addRange(List<InStorageRecord> list);
    }
}
