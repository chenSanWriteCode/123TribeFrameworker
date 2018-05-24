using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _123TribeFrameworker.CommonTools;
using _123TribeFrameworker.Entity;

namespace _123TribeFrameworker.Services
{
    public interface IRoleMenuLayerService
    {
        Task<Result<RoleMenu>> deleteMenuAsync(int id);
        List<RoleMenu> searchRoleMenusAsync(string id = null);
        List<RoleProp> searchRoleMenusNotInRoleId(string roleId, DirLevel level);
        Task<Result<RoleMenu>> addRoleMenuRangeAsync(string roleId, DirLevel level, int[] menuIds);
        Task<RoleMenu> searchRoleMenuByIdAsync(int id);
    }
}
