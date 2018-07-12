using System.Collections.Generic;
using _123TribeFrameworker.Entity;
using _123TribeFrameworker.Models.BussinessModels;
using _123TribeFrameworker.Models.QueryModel;

namespace _123TribeFrameworker.Services
{
    public interface IOutRecordService
    {
        Pager<List<OutRecordModel>> searchByCondition(Pager<List<OutRecordModel>> pager, OutRecordQuery t);
        /// <summary>
        /// 汇总
        /// </summary>
        /// <param name="pager"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        Pager<List<OutRecordModel>> searchSumByCondition(Pager<List<OutRecordModel>> pager, OutRecordQuery t);
        /// <summary>
        /// 热销榜前十
        /// </summary>
        /// <returns></returns>
        List<MaterialInfo> searchHotTen();
    }
}
