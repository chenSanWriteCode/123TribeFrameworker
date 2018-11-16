using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using _123TribeFrameworker.Entity;
using _123TribeFrameworker.Models;
using Unity.Attributes;

namespace _123TribeFrameworker.DAO.DirDAO
{
    public class RoleMenuDAO
    {
        /// <summary>
        /// delete by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Result<RoleMenu>> deleteMenuAsync(int id)
        {
            Result<RoleMenu> result = new Result<RoleMenu>();
            try
            {
                RoleMenuDbContext context = new RoleMenuDbContext();
                RoleMenu model = await context.roleMenus.FindAsync(id);
                context.roleMenus.Remove(model);
                int count = await context.SaveChangesAsync();
                if (count == 0)
                {
                    result.success = false;
                    result.message = "This Role Do Not Include This Menu";
                }
            }
            catch (Exception err)
            {
                result.success = false;
                result.message = err.Message;
            }
            return result;
        }
        /// <summary>
        /// 根据roleId查询角色权限
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<RoleMenu> searchRoleMenus(string id = null)
        {
            RoleMenuDbContext roleMenuDB = new RoleMenuDbContext();
            if (string.IsNullOrEmpty(id))
            {
                return roleMenuDB.roleMenus.ToList();
            }
            else
            {
                return roleMenuDB.roleMenus.Where(x => x.roleId == id).ToList();
            }
        }
        /// <summary>
        /// add range async
        /// </summary>
        /// <param name="menuList"></param>
        /// <returns></returns>
        public async Task<Result<RoleMenu>> addRoleMenuRangeAsync(List<RoleMenu> menuList)
        {
            Result<RoleMenu> result = new Result<RoleMenu>();
            try
            {
                RoleMenuDbContext roleMenuDB = new RoleMenuDbContext();
                roleMenuDB.roleMenus.AddRange(menuList);
                int count = await roleMenuDB.SaveChangesAsync();

            }
            catch (Exception err)
            {
                result.success = false;
                result.message = err.Message;
            }
            return result;
        }

        public async Task<RoleMenu> searchRoleMenuById(int id)
        {
            RoleMenuDbContext roleMenuDB = new RoleMenuDbContext();
            RoleMenu model = await roleMenuDB.roleMenus.FindAsync(id);
            return model;
        }
    }
}