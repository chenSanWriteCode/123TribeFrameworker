using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _123TribeFrameworker.Entity;

namespace _123TribeFrameworker.Services
{
    public interface IMaterialInfoService
    {
        Task<Result<MaterialInfo>> addAsync(MaterialInfo model);
        Pager<List<MaterialInfo>> searchByCondition(Pager<List<MaterialInfo>> pager, MaterialInfo condition);

        Result<MaterialInfo> searchByid(int id);

        Task<Result<int>> deleteByIdAsync(int id);

        Task<Result<int>> update(MaterialInfo model);
    }
}
