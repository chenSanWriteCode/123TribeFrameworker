using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _123TribeFrameworker.CommonTools;
using _123TribeFrameworker.Entity;
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
            practiceEntities entities = new practiceEntities();
            var result = entities.SecondLevel.Where(x => x.id > 0 && x.activityFlag == 1);
            int start = (pager.page - 1) * pager.recPerPage;
            if (pager.data.id.HasValue)
            {
                result = result.Where(x => x.id == pager.data.id.Value);
            }
            else
            {
                result = !pager.data.orderId.HasValue ? result : result.Where(x => x.orderId == pager.data.orderId);
                result = string.IsNullOrEmpty(pager.data.title) ? result : result.Where(x => x.title == pager.data.title);
                result = string.IsNullOrEmpty(pager.data.createdBy) ? result : result.Where(x => x.createdBy == pager.data.createdBy);
                result = string.IsNullOrEmpty(pager.data.lastUpdatedBy) ? result : result.Where(x => x.lastUpdateBy == pager.data.lastUpdatedBy);
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
            practiceEntities entities = new practiceEntities();
            var result = entities.SecondLevel.Where(x => x.activityFlag == 1);
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
            practiceEntities entities = new practiceEntities();
            return entities.SecondLevel.FirstOrDefault(x => x.activityFlag == 1 && x.id == id);
        }
        /// <summary>
        /// 获取总条数
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int getSecondLevelDirCount(SecondLevelDirModel model)
        {
            practiceEntities entities = new practiceEntities();
            var result = entities.SecondLevel.Where(x => x.id > 0 && x.activityFlag == 1);
            if (model.id.HasValue)
            {
                result = result.Where(x => x.id == model.id.Value);
            }
            else
            {
                result = model.orderId.HasValue ? result : result.Where(x => x.orderId == model.orderId);
                result = string.IsNullOrEmpty(model.title) ? result : result.Where(x => x.title == model.title);
                result = string.IsNullOrEmpty(model.createdBy) ? result : result.Where(x => x.createdBy == model.createdBy);
                result = string.IsNullOrEmpty(model.lastUpdatedBy) ? result : result.Where(x => x.lastUpdateBy == model.lastUpdatedBy);
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
            practiceEntities entities = new practiceEntities();
            var result = entities.SecondLevel.Where(x => x.id ==id);
            SecondLevel entity = result.First();
            if (entity != null)
            {
                entity.activityFlag = 0;
            }
            return entities.SaveChanges();
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int updateSecondLevelDir(SecondLevelDirModel model)
        {
            practiceEntities entities = new practiceEntities();
            var result = entities.SecondLevel.Where(x => x.id > 0 && x.activityFlag == 1);
            result = model.id.HasValue ? result.Where(x => x.id == model.id.Value) : null;
            if (result != null)
            {
                SecondLevel entity = result.First();
                modelToEntity(model, ref entity);
            }
            return entities.SaveChanges();
        }
        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int addSecondLevelDir(SecondLevelDirModel model)
        {
            practiceEntities entities = new practiceEntities();
            if (model != null)
            {
                SecondLevel entity = new SecondLevel();
                modelToEntity(model, ref entity);
                //DirTools tools = new DirTools();
                //entity.afterContent = tools.afterContent;
                //entity.beforContent = tools.beforContent;
                entity.activityFlag = 1;
                var result = entities.SecondLevel.Add(entity);
            }
            return entities.SaveChanges();
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
                    entity.lastUpdateBy = model.lastUpdatedBy?.ToString();
                }
                if (!string.IsNullOrEmpty(model.title))
                {
                    entity.title = model.title?.ToString();
                }
                entity.firstLevelID = model.firstLevelID;
            }
        }
        #endregion
    }
}