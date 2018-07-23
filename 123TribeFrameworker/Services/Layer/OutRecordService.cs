using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using _123TribeFrameworker.CommonTools;
using _123TribeFrameworker.DAO;
using _123TribeFrameworker.Entity;
using _123TribeFrameworker.Infrastructrue;
using _123TribeFrameworker.Models.BussinessModels;
using _123TribeFrameworker.Models.QueryModel;
using Unity.Attributes;

namespace _123TribeFrameworker.Services.Layer
{
    public class OutRecordService : IOutRecordService
    {
        [Dependency]
        public IOutRecordDAO dao { get; set; }

        public Pager<List<OutRecordModel>> searchByCondition(Pager<List<OutRecordModel>> pager, OutRecordQuery t)
        {
            pager.data = dao.searchByCondition(pager, t);
            pager.recTotal = dao.searchCountByCondition(t);
            return pager;
        }
        public float searchSumProfitByCondition(OutRecordQuery t)
        {
            return dao.searchSumProfitByCondition(t); ;
        }
        public List<MaterialInfo> searchHotTen()
        {
            return dao.searchHotTen();
        }
        public Pager<List<OutRecordModel>> searchSumByCondition(Pager<List<OutRecordModel>> pager, OutRecordQuery t)
        {
            pager.data = dao.searchSumByCondition(pager, t);
            pager.recTotal = dao.searchSumCountByCondition(t);
            return pager;
        }

        public async Task<float[]> searchLastMonthProfit()
        {
            var result = await Task.Factory.StartNew(() => dao.searchLastMonthProfit());
            return result;
        }

        public async Task<List<PieDataTool>> searchYesterdayNum()
        {
            return await Task.Factory.StartNew(() => dao.searchYesterdayNum());
        }
    }
}