using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _123TribeFrameworker.DAO.DirDAO;
using _123TribeFrameworker.Entity;
using _123TribeFrameworker.Models.DirModels;

namespace _123TribeFrameworker.Services.Layer
{
    public class ThirdLevelDir
    {
        /// <summary>
        /// 通过条件进行查询
        /// </summary>
        /// <param name="pager"></param>
        /// <returns></returns>
        public Pager<List<ThirdLevelDirModel>> getThirdLevelDir(Pager<ThirdLevelDirModel> pager)
        {
            ThirdLevelDirDAO dao = new ThirdLevelDirDAO();

            Pager<List<ThirdLevelDirModel>> resultPager = new Pager<List<ThirdLevelDirModel>>();
            List<ThirdLevelDirModel> resultList = new List<ThirdLevelDirModel>();
            List<ThirdLevel> ThirdLevelList = new List<ThirdLevel>();
            ThirdLevelList = dao.getThirdLevelDir(pager);
            foreach (var item in ThirdLevelList)
            {
                ThirdLevelDirModel demo = entityToModel(item);
                resultList.Add(demo);
            }
            resultPager.data = resultList;
            resultPager.page = pager.page;
            //resultPager.recTotal = 89;
            resultPager.recTotal = dao.getThirdLevelDirCount(pager.data);
            return resultPager;
        }
        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Result<ThirdLevelDirModel> addThirdLevelDir(ThirdLevelDirModel model)
        {
            ThirdLevelDirDAO dao = new ThirdLevelDirDAO();
            Result<ThirdLevelDirModel> result = new Result<ThirdLevelDirModel>();
            if (dao.addThirdLevelDir(model) != 1)
            {
                result.result = false;
            }
            return result;
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Result<ThirdLevelDirModel> updateThirdLevelDir(ThirdLevelDirModel model)
        {
            ThirdLevelDirDAO dao = new ThirdLevelDirDAO();
            Result<ThirdLevelDirModel> result = new Result<ThirdLevelDirModel>();
            if (dao.updateThirdLevelDir(model) != 1)
            {
                result.result = false;
            }
            return result;
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Result<ThirdLevelDirModel> deleteThirdLevelDir(int id)
        {
            ThirdLevelDirDAO dao = new ThirdLevelDirDAO();
            Result<ThirdLevelDirModel> result = new Result<ThirdLevelDirModel>();
            if (dao.deleteThirdlevelDir(id) != 1)
            {
                result.result = false;
            }
            return result;
        }
        #region  工具
        /// <summary>
        /// 后端entity转前端model
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ThirdLevelDirModel entityToModel(ThirdLevel model)
        {
            ThirdLevelDirModel entity = new ThirdLevelDirModel();
            if (model != null)
            {
                entity.secondLevelName = model.SecondLevel.title;
                entity.id = model.id;
                entity.orderId = model.orderId;
                entity.createdBy = model.createdBy;
                entity.createdDate = model.createdDate;
                entity.lastUpdatedBy = model.lastUpdateBy;
                entity.lastUpdatedDate = model.lastUpdatedDate;
                entity.title = model.title;
                entity.secondLevelID = model.secondLevelID.Value;
                entity.url = model.url;
            }
            return entity;
        }
        #endregion
    }
}