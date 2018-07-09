using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _123TribeFrameworker.Entity;
using _123TribeFrameworker.Models.QueryModel;

namespace _123TribeFrameworker.DAO
{
    public interface IMaterialInfoDAO : ICommonDAO<MaterialInfo>
    {
        //Task<MaterialInfo> searchByMaterial(string material);
        List<MaterialInfo> searchAllMaterialInfo();

        List<MaterialInfo> searchByCondition(Pager<List<MaterialInfo>> pager, MaterialInfoQuery t);

        int searchCountByCondition(MaterialInfoQuery t);

        Task<List<MaterialInfo>> searchByIds(int[] ids);
        List<MaterialInfo> searchListByCondition(MaterialInfoQuery condition);
    }
}
