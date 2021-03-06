﻿using System.Collections.Generic;
using _123TribeFrameworker.CommonTools;
using _123TribeFrameworker.Entity;
using _123TribeFrameworker.Models.BussinessModels;
using _123TribeFrameworker.Models.QueryModel;

namespace _123TribeFrameworker.DAO
{
    public interface IOutRecordDAO
    {
       List<OutRecordModel> searchByCondition(Pager<List<OutRecordModel>> pager, OutRecordQuery outRecordQuery);
        int searchCountByCondition(OutRecordQuery t);
        List<OutRecordModel> searchSumByCondition(Pager<List<OutRecordModel>> pager, OutRecordQuery t);
        int searchSumCountByCondition(OutRecordQuery t);

        List<MaterialInfo> searchHotTen();

        float searchSumProfitByCondition(OutRecordQuery t);
        /// <summary>
        /// 查询近30天收益情况
        /// </summary>
        /// <returns></returns>
        float[] searchLastMonthProfit();
        /// <summary>
        /// 昨日产品销售数量
        /// </summary>
        /// <returns></returns>
        List<PieDataTool> searchYesterdayNum();
    }
}
