using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using System.Web;
using _123TribeFrameworker.CommonTools;
using _123TribeFrameworker.Entity;
using _123TribeFrameworker.Models;
using _123TribeFrameworker.Models.DirModels;
using _123TribeFrameworker.Models.QueryModel;

namespace _123TribeFrameworker.DAO.DirDAO
{
    public class SecondLevelDirDAO:ISecondLevelDirDAO
    {
        /// <summary>
        /// 根据动态条件获取所有2级菜单集合
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Result<List<SecondLevel>> getSecondLevelDir(Pager<List<SecondLevel>> pager, SecondMenuQuery condition)
        {
            Result<List<SecondLevel>> result = new Result<List<SecondLevel>>();
            try
            {
                DirDbContext context = new DirDbContext();
                var sql = context.secondLevels.Where(x => x.activityFlag == 1);
                int start = (pager.page - 1) * pager.recPerPage;
                sql = string.IsNullOrEmpty(condition.title) ? sql : sql.Where(x => x.title == condition.title);
                sql = condition.firstLevelID == 0 ? sql : sql.Where(x => x.firstLevelId == condition.firstLevelID);
                sql = sql.OrderBy(x => x.orderId).ThenBy(x => x.title).Skip(start).Take(pager.recPerPage);
                result.data = sql.ToList();
            }
            catch (Exception err)
            {
                result.addError(err.Message);
            }

            return result;
        }
        /// <summary>
        /// 获取所有主菜单list
        /// </summary>
        /// <returns></returns>
        public List<SecondLevel> getSecondLevelDirList()
        {
            DirDbContext context = new DirDbContext();
            var result = context.secondLevels.Where(x => x.activityFlag == 1);
            result = result.OrderBy(x => x.orderId).ThenBy(x => x.id);
            return result.ToList();
        }
        /// <summary>
        /// 根据id获取一条二级菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Result<SecondLevel> getSingleSecondDir(int id)
        {
            Result<SecondLevel> result = new Result<SecondLevel>();
            try
            {
                DirDbContext context = new DirDbContext();
                result.data= context.secondLevels.FirstOrDefault(x => x.activityFlag == 1 && x.id == id);
            }
            catch (Exception err)
            {
                result.addError(err.Message);
            }
            return result;
        }
        /// <summary>
        /// 获取总条数
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public int getSecondLevelDirCount(SecondMenuQuery condition)
        {
            int result = 0;
            try
            {
                DirDbContext context = new DirDbContext();
                var sql = context.secondLevels.Where(x => x.activityFlag == 1);
                sql = string.IsNullOrEmpty(condition.title) ? sql : sql.Where(x => x.title == condition.title);
                sql = condition.firstLevelID == 0 ? sql : sql.Where(x => x.firstLevelId == condition.firstLevelID);
                result = sql.Count();
            }
            catch (Exception)
            {
            }
            return result;
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Result<int> deleteSecondlevelDir(int id)
        {
            Result<int> result = new Result<int>();
            try
            {
                using (var ts = new TransactionScope())
                {
                    DirDbContext context = new DirDbContext();
                    RoleMenuDbContext roleContext = new RoleMenuDbContext();
                    var entity = context.secondLevels.Where(x => x.id == id).FirstOrDefault();
                    if (entity != null)
                    {
                        context.secondLevels.Remove(entity);
                        context.SaveChanges();
                    }
                    var roleMenu = roleContext.roleMenus.Where(x => x.menuLevel == 2 && x.menuId == id).FirstOrDefault();
                    if (roleMenu != null)
                    {
                        roleContext.roleMenus.Remove(roleMenu);
                        roleContext.SaveChanges();
                    }
                    ts.Complete();
                }
            }
            catch (Exception err)
            {
                result.addError(err.Message);
            }
            return result;
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Result<SecondLevel>> updateSecondLevelDir(SecondLevel model)
        {
            Result<SecondLevel> result = new Result<SecondLevel>();
            try
            {
                using (DirDbContext context = new DirDbContext())
                {
                    var data = context.secondLevels.Where(x => x.activityFlag == 1 && x.id == model.id).FirstOrDefault();
                    if (data != null)
                    {
                        data.orderId = model.orderId;
                        data.title = model.title;
                        data.open = model.open;
                        data.url = model.url;
                        data.firstLevelId = model.firstLevelId;
                        data.lastUpdatedBy = model.lastUpdatedBy;
                        data.lastUpdatedDate = model.lastUpdatedDate;
                    }
                    var count = await context.SaveChangesAsync();
                }
            }
            catch (Exception err)
            {
                result.addError(err.Message);
            }


            return result;
        }
        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Result<SecondLevel>> addSecondLevelDir(SecondLevel model)
        {
            Result<SecondLevel> result = new Result<SecondLevel>();

            using (DirDbContext context = new DirDbContext())
            {
                try
                {
                    var data = context.secondLevels.FirstOrDefault(x => x.title == model.title);
                    if (data != null)
                    {
                        result.addError($"目录名{model.title}已经存在");
                    }
                    else
                    {
                        result.data = context.secondLevels.Add(model);
                        await context.SaveChangesAsync();
                    }
                }
                catch (Exception err)
                {
                    result.addError(err.Message);
                }
            }
            return result;
        }
    }
}