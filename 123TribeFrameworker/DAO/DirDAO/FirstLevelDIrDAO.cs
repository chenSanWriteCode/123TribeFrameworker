using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _123TribeFrameworker.Entity;
using _123TribeFrameworker.Models;

namespace _123TribeFrameworker.DAO.DirDAO
{
    public class FirstLevelDirDAO
    {
        /// <summary>
        /// 根据动态条件获取所有一级菜单集合
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<firstLevel> getFirstLevelDir(Pager<FirstLevelDirModel> pager)
        {
            practiceEntities entities = new practiceEntities();
            var result = entities.firstLevel.Where(x => x.id > 0 && x.activityFlag == 1);
            int start = (pager.page - 1) * pager.recPerPage;
            if (pager.data.id.HasValue)
            {
                result = result.Where(x => x.id == pager.data.id.Value);
            }
            else
            {
                result = !pager.data.orderId.HasValue ? result : result.Where(x => x.orderId == pager.data.orderId);
                result = string.IsNullOrEmpty(pager.data.content) ? result : result.Where(x => x.midContent == pager.data.content);
                result = string.IsNullOrEmpty(pager.data.createdBy) ? result : result.Where(x => x.createdBy == pager.data.createdBy);
                result = string.IsNullOrEmpty(pager.data.lastUpdatedBy) ? result : result.Where(x => x.lastUpdatedBy == pager.data.lastUpdatedBy);
                result = !pager.data.createdDate.HasValue ? result : result.Where(x => x.createdDate == pager.data.createdDate);
                result = !pager.data.lastUpdatedDate.HasValue ? result : result.Where(x => x.lastUpdatedDate == pager.data.lastUpdatedDate);
            }
            result = result.OrderBy(x => x.orderId).Skip(start).Take(pager.recPerPage);
            return result.ToList();
        }
        /// <summary>
        /// 获取总条数
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int getFirstLevelDirCount(FirstLevelDirModel model)
        {
            practiceEntities entities = new practiceEntities();
            var result = entities.firstLevel.Where(x => x.id > 0 && x.activityFlag == 1);
            if (model.id.HasValue)
            {
                result = result.Where(x => x.id == model.id.Value);
            }
            else
            {
                result = model.orderId.HasValue ? result : result.Where(x => x.orderId == model.orderId);
                result = string.IsNullOrEmpty(model.content) ? result : result.Where(x => x.midContent == model.content);
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
        public int deleteFirstlevelDir(FirstLevelDirModel model)
        {
            practiceEntities entities = new practiceEntities();
            var result = entities.firstLevel.Where(x => x.id > 0);
            result = model.id.HasValue ? result.Where(x => x.id == model.id.Value) : null;
            if (result != null)
            {
                firstLevel entity = result.First();
                entity.activityFlag = 0;
            }
            return entities.SaveChanges();
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int updateFirstLevelDir(FirstLevelDirModel model)
        {
            practiceEntities entities = new practiceEntities();
            var result = entities.firstLevel.Where(x => x.id > 0 && x.activityFlag == 1);
            result = model.id.HasValue ? result.Where(x => x.id == model.id.Value) : null;
            if (result != null)
            {
                firstLevel entity = result.First();
                entity = modelToEntity(model);
            }
            return entities.SaveChanges();
        }
        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int addFirstLevelDir(FirstLevelDirModel model)
        {
            practiceEntities entities = new practiceEntities();
            if (model != null)
            {
                firstLevel entity = modelToEntity(model);
                entities.firstLevel.Add(entity);
            }
            return entities.SaveChanges();
        }

        #region 工具
        /// <summary>
        /// 前端model转后端entity
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public firstLevel modelToEntity(FirstLevelDirModel model)
        {
            firstLevel entity = new firstLevel();
            if (model != null)
            {
                entity.id = model.id.Value;
                entity.orderId = model.orderId.Value;
                entity.createdBy = model.createdBy?.ToString();
                entity.createdDate = model.createdDate.Value;
                entity.lastUpdatedBy = model.lastUpdatedBy?.ToString();
                entity.lastUpdatedDate = model.lastUpdatedDate.Value;
                entity.midContent = model.content?.ToString();
            }
            return entity;
        }
        #endregion
    }
}