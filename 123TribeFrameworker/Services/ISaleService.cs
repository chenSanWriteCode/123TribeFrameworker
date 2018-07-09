using System.Collections.Generic;
using System.Threading.Tasks;
using _123TribeFrameworker.Entity;
using _123TribeFrameworker.Models.BussinessModels;

namespace _123TribeFrameworker.Services
{
    public interface ISaleService
    {
        /// <summary>
        /// 收银
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        Task<Result<string>> receive(List<SaleModel> list,string userName);
    }
}
