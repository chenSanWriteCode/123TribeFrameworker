using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _123TribeFrameworker.Entity;
using _123TribeFrameworker.Models.QueryModel;

namespace _123TribeFrameworker.Services
{
    public interface IMaterialInfoService
    {
        Pager<List<MaterialInfo>> searchByCondition(Pager<List<MaterialInfo>> pager, MaterialInfoQuery condition);
        Task<Result<MaterialInfo>> addAsync(MaterialInfo model);
        Task<MaterialInfo> searchByid(int id);
        Task<Result<int>> deleteByIdAsync(int id);
        Task<Result<int>> update(MaterialInfo model);

    }
}
