using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _123TribeFrameworker.DAO.BussinessDAO;
using _123TribeFrameworker.Entity;
using _123TribeFrameworker.Services;
using _123TribeFrameworker.Services.Layer;
using Unity.Attributes;

namespace _123TribeFrameworker.Infrastructrue
{
    public static class BaseDataHelper
    {
       /// <summary>
       /// 订单状态
       /// </summary>
       /// <param name="html"></param>
       /// <returns></returns>
        [OutputCache(Duration =3600)]
        public static Dictionary<string,string> orderStatus(this HtmlHelper html)
        {
            LayerDbContext context = new LayerDbContext();
            return context.orderStatus.ToDictionary(x => x.statusKey, x => x.status);
        }
        /// <summary>
        /// 物料
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        ///[OutputCache(Duration = 300)]
        public static List<MaterialInfo> materialInfoList(this HtmlHelper html)
        {
            MaterialInfoDAO dao = new MaterialInfoDAO();
            return dao.searchAllMaterialInfo();
        }
        /// <summary>
        /// 获取最近三十天日期（不包括今天）
        /// </summary>
        /// <param name="currentDate"></param>
        /// <returns></returns>
        public static List<DateTime> getLastMonthDate(DateTime currentDate)
        {
            List<DateTime> result = new List<DateTime>();
            for (int i = 30; i >0; i--)
            {
                DateTime model = currentDate.AddDays(-i).Date;
                result.Add(model);
            }
            return result;
        }
        /// <summary>
        /// 获取最近6个月的时间（xx月1日）
        /// </summary>
        /// <param name="currentDate"></param>
        /// <returns></returns>
        public static List<DateTime> getSixMnthDate(DateTime currentDate)
        {
            List<DateTime> result = new List<DateTime>();
            for (int i = 5; i > 0; i--)
            {
                DateTime model = currentDate.AddMonths(-i).AddDays(1-currentDate.Day).Date;
                result.Add(model);
            }
            //本月
            DateTime model1 = currentDate.AddDays(1 - currentDate.Day).Date;
            result.Add(model1);
            return result;
        }
    }
}