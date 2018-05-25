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
    }
}