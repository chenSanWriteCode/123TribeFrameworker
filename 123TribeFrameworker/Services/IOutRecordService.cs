using System.Collections.Generic;
using System.Threading.Tasks;
using _123TribeFrameworker.CommonTools;
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
        /// <summary>
        /// 利润总和
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        float searchSumProfitByCondition(OutRecordQuery t);
        /// <summary>
        /// 查询近30天收益情况
        /// </summary>
        /// <returns></returns>
        Task<float[]> searchLastMonthProfit();
        /// <summary>
        /// 昨日产品销售数量
        /// </summary>
        /// <returns></returns>
        Task<List<PieDataTool>> searchYesterdayNum();
    }
}
