using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _123TribeFrameworker.CommonTools;
using _123TribeFrameworker.Entity;
using _123TribeFrameworker.Models;
using _123TribeFrameworker.Models.DirModels;

namespace _123TribeFrameworker.DAO.DirDAO
{
    public class SecondLevelDirDAO
    {
        /// <summary>
        /// 根据动态条件获取所有2级菜单集合
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<SecondLevel> getSecondLevelDir(Pager<SecondLevelDirModel> pager)
        {
            DirDbContext context = new DirDbContext();
            var result = context.secondLevels.Where(x => x.activityFlag == 1);
            int start = (pager.page - 1) * pager.recPerPage;
            if (pager.data.id.HasValue)
            {
                result = result.Where(x => x.id == pager.data.id.Value);
            }
            else
            {
                result = !pager.data.orderId.HasValue ? result : result.Where(x => x.orderId == pager.data.orderId);
                result = string.IsNullOrEmpty(pager.data.title) ? result : result.Where(x => x.title == pager.data.title);
                result = !pager.data.firstLevelID.HasValue ? result : result.Where(x => x.firstLevelId == pager.data.firstLevelID);
                result = string.IsNullOrEmpty(pager.data.createdBy) ? result : result.Where(x => x.createdBy == pager.data.createdBy);
                result = string.IsNullOrEmpty(pager.data.lastUpdatedBy) ? result : result.Where(x => x.lastUpdatedBy == pager.data.lastUpdatedBy);
                result = !pager.data.createdDate.HasValue ? result : result.Where(x => x.createdDate == pager.data.createdDate);
                result = !pager.data.lastUpdatedDate.HasValue ? result : result.Where(x => x.lastUpdatedDate == pager.data.lastUpdatedDate);
            }
            result = result.OrderBy(x => x.orderId).ThenBy(x => x.id).Skip(start).Take(pager.recPerPage);
            return result.ToList();
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
        public SecondLevel getSingleSecondDir(int id)
        {
            DirDbContext context = new DirDbContext();
            return context.secondLevels.FirstOrDefault(x => x.activityFlag == 1 && x.id == id);
        }
        /// <summary>
        /// 获取总条数
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int getSecondLevelDirCount(SecondLevelDirModel model)
        {
            DirDbContext context = new DirDbContext();
            var result = context.secondLevels.Where(x => x.activityFlag == 1);
            if (model.id.HasValue)
            {
                result = result.Where(x => x.id == model.id.Value);
            }
            else
            {
                result = model.orderId.HasValue ? result : result.Where(x => x.orderId == model.orderId);
                result = string.IsNullOrEmpty(model.title) ? result : result.Where(x => x.title == model.title);
                result = string.IsNullOrEmpty(model.createdBy) ? result : result.Where(x => x.createdBy == model.createdBy);
                result = string.IsNullOrEmpty(model.lastUpdatedBy) ? result : result.Where(x => x.lastUpdatedBy == model.lastUpdatedBy);
                result = model.createdDate.HasValue ? result : result.Where(x => x.createdDate == model.createdDate);
                result = model.lastUpdatedDate.HasValue ? result : result.Where(x => x.lastUpdatedDate == model.lastUpdatedDate);
            }
            return result.Count();
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int deleteSecondlevelDir(int id)
        {
            //DONE: delete from roleMenu
            DirDbContext context = new DirDbContext();
            var result = context.secondLevels.Where(x => x.id == id);
            SecondLevel entity = result.First();
            context.secondLevels.Remove(entity);
            RoleMenuDbContext roleContext = new RoleMenuDbContext();
            var roleMenu = roleContext.roleMenus.Where(x => x.menuLevel == 2 && x.menuId == id).First();
            roleContext.roleMenus.Remove(roleMenu);
            roleContext.SaveChanges();
            return context.SaveChanges();
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int updateSecondLevelDir(SecondLevelDirModel model)
        {
            DirDbContext context = new DirDbContext();
            var result = context.secondLevels.Where(x => x.activityFlag == 1 &&  x.id == model.id.Value);
            SecondLevel entity = result.First();
            modelToEntity(model, ref entity);
            return context.SaveChanges();
        }
        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int addSecondLevelDir(SecondLevelDirModel model)
        {
            DirDbContext context = new DirDbContext();
            if (model != null)
            {
                SecondLevel entity = new SecondLevel();
                modelToEntity(model, ref entity);
                entity.activityFlag = 1;
                entity.url = "#";
                var result = context.secondLevels.Add(entity);
            }
            return context.SaveChanges();
        }

        #region 工具
        /// <summary>
        /// 前端model转后端entity
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void modelToEntity(SecondLevelDirModel model, ref SecondLevel entity)
        {
            if (model != null)
            {
                if (model.id.HasValue)
                {
                    entity.id = model.id.Value;
                }
                if (model.orderId.HasValue)
                {
                    entity.orderId = model.orderId.Value;
                }
                if (model.createdDate.HasValue)
                {
                    entity.createdDate = model.createdDate.Value;
                }
                if (model.lastUpdatedDate.HasValue)
                {
                    entity.lastUpdatedDate = model.lastUpdatedDate ?? null;
                }
                if (!string.IsNullOrEmpty(model.createdBy))
                {
                    entity.createdBy = model.createdBy?.ToString();
                }
                if (!string.IsNullOrEmpty(model.lastUpdatedBy))
                {
                    entity.lastUpdatedBy = model.lastUpdatedBy?.ToString();
                }
                if (!string.IsNullOrEmpty(model.title))
                {
                    entity.title = model.title?.ToString();
                }
                if (!string.IsNullOrEmpty(model.url))
                {
                    entity.url = model.url;
                }
                entity.firstLevelId = model.firstLevelID.Value;
            }
        }
        #endregion
    }
}