using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _123TribeFrameworker.Entity;

namespace _123TribeFrameworker.Services
{
    public interface IInStorageRecordService
    {
        /// <summary>
        /// 增加list
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        Task<Result<int>> addRangeAsync(List<InStorageRecord> list);
    }
}
