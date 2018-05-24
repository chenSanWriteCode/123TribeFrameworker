using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using _123TribeFrameworker.CommonTools;
using _123TribeFrameworker.DAO.DirDAO;
using _123TribeFrameworker.Entity;

namespace _123TribeFrameworker.Services.Layer
{
    public class RoleMenuLayer: IRoleMenuLayerService
    {
        public RoleMenuDAO roleMenuDAO { get { return new RoleMenuDAO(); } }
        /// <summary>
        /// delete roleMenu by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Result<RoleMenu>> deleteMenuAsync(int id)
        {
            Result<RoleMenu> result = await roleMenuDAO.deleteMenuAsync(id);
            return result;
        }
        /// <summary>
        /// search roleMenu by roleID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<RoleMenu> searchRoleMenusAsync(string id = null)
        {
            Task<List<RoleMenu>> task = Task.Factory.StartNew(() => roleMenuDAO.searchRoleMenus(id));
            List<RoleMenu> list = task.Result;
            return list;
        }
        /// <summary>
        /// 查询不在role中的menu
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="dirLevel"></param>
        /// <returns></returns>
        public List<RoleProp> searchRoleMenusNotInRoleId(string roleId, DirLevel level)
        {
            List<RoleMenu> containsMenu = searchRoleMenusAsync(roleId);
            List<RoleProp> result = new List<RoleProp>();
            switch (level)
            {
                case DirLevel.FirstLevel:
                    FirstLevelDirDAO daoF = new FirstLevelDirDAO();
                    List<FirstLevel> firstAll = daoF.getFirstLevelDirList();
                    result = (from x in firstAll
                              where !(containsMenu.Where(y => y.menuLevel == 1).Select(y => y.menuId).Contains(x.id))
                              select new RoleProp
                              {
                                  menuId = x.id,
                                  menuName = x.midContent
                              }).ToList();
                    break;
                case DirLevel.SecondLevel:
                    SecondLevelDirDAO daoS = new SecondLevelDirDAO();
                    List<SecondLevel> secondAll = daoS.getSecondLevelDirList();
                    result = (from x in secondAll
                              where !(containsMenu.Where(y=>y.menuLevel==2).Select(y => y.menuId).Contains(x.id))
                              select new RoleProp
                              {
                                  menuId = x.id,
                                  menuName = x.title
                              }).ToList();
                    break;
                case DirLevel.ThirdLLevel:
                    ThirdLevelDirDAO daoT = new ThirdLevelDirDAO();
                    List<ThirdLevel> thirdAll = daoT.getThirdLevelDirList();
                    result = (from x in thirdAll
                              where !(containsMenu.Where(y => y.menuLevel ==3 ).Select(y => y.menuId).Contains(x.id))
                              select new RoleProp
                              {
                                  menuId = x.id,
                                  menuName = x.title
                              }).ToList();
                    break;
                default:
                    break;
            }
            return result;
        }
        /// <summary>
        /// add roleMenu  range 
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="level"></param>
        /// <param name="menuIds"></param>
        /// <returns></returns>
        public async Task<Result<RoleMenu>> addRoleMenuRangeAsync(string roleId, DirLevel level, int[] menuIds)
        {
            Result<RoleMenu> result = new Result<RoleMenu>();
            List<RoleMenu> list = new List<RoleMenu>();
            foreach (var item in menuIds)
            {
                list.Add(new RoleMenu
                {
                    menuId = item,
                    menuLevel = Convert.ToInt32(level),
                    roleId = roleId
                });
            }
            result = await roleMenuDAO.addRoleMenuRangeAsync(list);
            return result;
        }
        public async Task<RoleMenu> searchRoleMenuByIdAsync(int id)
        {
            var result = await roleMenuDAO.searchRoleMenuById(id);
            return result;
        }
    }
}