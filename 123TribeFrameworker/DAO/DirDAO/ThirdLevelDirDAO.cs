using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _123TribeFrameworker.Entity;
using _123TribeFrameworker.Models.DirModels;

namespace _123TribeFrameworker.DAO.DirDAO
{
    public class ThirdLevelDirDAO
    {
        /// <summary>
        /// 根据动态条件获取所有3级菜单集合
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<ThirdLevel> getThirdLevelDir(Pager<ThirdLevelDirModel> pager)
        {
            practiceEntities entities = new practiceEntities();
            var result = entities.ThirdLevel.Where(x => x.activityFlag == 1);
            int start = (pager.page - 1) * pager.recPerPage;
            if (pager.data.id.HasValue)
            {
                result = result.Where(x => x.id == pager.data.id.Value);
            }
            else
            {
                result = !pager.data.orderId.HasValue ? result : result.Where(x => x.orderId == pager.data.orderId);
                result = string.IsNullOrEmpty(pager.data.title) ? result : result.Where(x => x.title == pager.data.title);
                result = !pager.data.secondLevelID.HasValue ? result : result.Where(x => x.secondLevelId == pager.data.secondLevelID);
                result = string.IsNullOrEmpty(pager.data.url) ? result : result.Where(x => x.url == pager.data.url);
                result = string.IsNullOrEmpty(pager.data.createdBy) ? result : result.Where(x => x.createdBy == pager.data.createdBy);
                result = string.IsNullOrEmpty(pager.data.lastUpdatedBy) ? result : result.Where(x => x.lastUpdatedBy == pager.data.lastUpdatedBy);
                result = !pager.data.createdDate.HasValue ? result : result.Where(x => x.createdDate == pager.data.createdDate);
                result = !pager.data.lastUpdatedDate.HasValue ? result : result.Where(x => x.lastUpdatedDate == pager.data.lastUpdatedDate);
            }
            result = result.OrderBy(x => x.orderId).ThenBy(x => x.id).Skip(start).Take(pager.recPerPage);
            return result.ToList();
        }
        /// <summary>
        /// 获取三级目录集合
        /// </summary>
        /// <returns></returns>
        public List<ThirdLevel> getThirdLevelDirList()
        {
            practiceEntities entity = new practiceEntities();
            var result = entity.ThirdLevel.Where(x => x.activityFlag == 1);
            result = result.OrderBy(x => x.orderId).ThenBy(x=>x.id);
            return result.ToList();
        }
        /// <summary>
        /// 根据id获取一条3级菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ThirdLevel getSingleThirdDir(int id)
        {
            practiceEntities entities = new practiceEntities();
            return entities.ThirdLevel.FirstOrDefault(x => x.activityFlag == 1 && x.id == id);
        }
        /// <summary>
        /// 获取总条数
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int getThirdLevelDirCount(ThirdLevelDirModel model)
        {
            practiceEntities entities = new practiceEntities();
            var result = entities.ThirdLevel.Where(x => x.activityFlag == 1);
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
        public int deleteThirdlevelDir(int id)
        {
            practiceEntities entities = new practiceEntities();
            var result = entities.ThirdLevel.Where(x => x.id == id);
            ThirdLevel entity = result.First();
            if (entity != null)
            {
                entity.activityFlag = 0;
            }
            entities.ThirdLevel.Remove(entity);
            return entities.SaveChanges();
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int updateThirdLevelDir(ThirdLevelDirModel model)
        {
            practiceEntities entities = new practiceEntities();
            var result = entities.ThirdLevel.Where(x => x.activityFlag == 1);
            result = model.id.HasValue ? result.Where(x => x.id == model.id.Value) : null;
            ThirdLevel entity = result.First();
            modelToEntity(model, ref entity);
            return entities.SaveChanges();
        }
        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int addThirdLevelDir(ThirdLevelDirModel model)
        {
            practiceEntities entities = new practiceEntities();
            if (model != null)
            {
                ThirdLevel entity = new ThirdLevel();
                modelToEntity(model, ref entity);
                entity.activityFlag = 1;
                var result = entities.ThirdLevel.Add(entity);
            }
            return entities.SaveChanges();
        }

        #region 工具
        /// <summary>
        /// 前端model转后端entity
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void modelToEntity(ThirdLevelDirModel model, ref ThirdLevel entity)
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
                entity.url = model.url;
                entity.secondLevelId = model.secondLevelID;
            }
        }
        #endregion
    }
}