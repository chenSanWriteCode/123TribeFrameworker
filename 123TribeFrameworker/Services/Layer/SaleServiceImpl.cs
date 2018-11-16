using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using _123TribeFrameworker.DAO;
using _123TribeFrameworker.Entity;
using _123TribeFrameworker.Models.BussinessModels;
using Unity.Attributes;

namespace _123TribeFrameworker.Services.Layer
{
    public class SaleServiceImpl:ISaleService
    {
        [Dependency]
        public ISaleDAO dao { get; set; }
        [Dependency]
        public IMaterialInfoDAO materialDao { get; set; }

        public async Task<Result<string>> receive(List<SaleModel> list, string userName)
        {
            string orderNo = "S" + DateTime.Now.ToString("yyyyMMddHHmmss");
            list.ForEach(x => { x.cashOrder = orderNo; x.createdBy = userName; });
            return await dao.receive(list);
        }
    }
}