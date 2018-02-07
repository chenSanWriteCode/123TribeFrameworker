using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _123TribeFrameworker.Entity;
using _123TribeFrameworker.Models;

namespace _123TribeFrameworker.DAO.DirDAO
{
    public class FirstLevelDIrDAO
    {
        /// <summary>
        /// 根据动态条件获取所有一级菜单集合
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<firstLevel> getFirstLevelDir(FirstLevelDirModel model)
        {
            practiceEntities entities = new practiceEntities();
            var result = entities.firstLevel.Where(x => x.id > 0);
            if (model.id.HasValue)
            {
                result = result.Where(x => x.id == model.id.Value);
            }
            else
            {
                result = string.IsNullOrEmpty(model.content)? result.Where(x => x.midContent == model.content):result;
                result = string.IsNullOrEmpty(model.createdBy) ? result.Where(x => x.createdBy == model.createdBy) : result;
                result = string.IsNullOrEmpty(model.lastUpdatedBy) ? result.Where(x => x.lastUpdatedBy == model.lastUpdatedBy) : result;
                result = model.createdDate.HasValue ? result.Where(x => x.createdDate == model.createdDate) : result;
                result = model.lastUpdatedDate.HasValue ? result.Where(x => x.lastUpdatedDate == model.lastUpdatedDate) : result;
            }
            return result.ToList();
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
            if (result!=null && result.Count() > 0)
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
            var result = entities.firstLevel.Where(x => x.id > 0);
            result = model.id.HasValue ? result.Where(x => x.id == model.id.Value) : null;
            if (result != null && result.Count()>0)
            {
                firstLevel entity = result.First();
                entity = modelToEntity(model);
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