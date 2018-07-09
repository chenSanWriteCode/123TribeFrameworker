using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _123TribeFrameworker.Entity;
using _123TribeFrameworker.Models.BussinessModels;

namespace _123TribeFrameworker.DAO
{
    public interface ISaleDAO:ICommonDAO<SaleModel>
    {
        /// <summary>
        /// 收银
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        Task<Result<string>> receive(List<SaleModel> list);
    }
}
