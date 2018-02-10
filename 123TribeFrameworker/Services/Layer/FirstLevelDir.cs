using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _123TribeFrameworker.DAO.DirDAO;
using _123TribeFrameworker.Entity;
using _123TribeFrameworker.Models.DirModels;

namespace _123TribeFrameworker.Services.Layer
{
    public class FirstLevelDir 
    {
        /// <summary>
        /// 通过条件进行查询
        /// </summary>
        /// <param name="pager"></param>
        /// <returns></returns>
        public Pager<List<FirstLevelDirModel>> getFirstLevelDir(Pager<FirstLevelDirModel> pager)
        {
            FirstLevelDirDAO dao = new FirstLevelDirDAO();

            Pager<List<FirstLevelDirModel>> resultPager = new Pager<List<FirstLevelDirModel>>();
            List<FirstLevelDirModel> resultList = new List<FirstLevelDirModel>();
            List<firstLevel> firstLevelList = new List<firstLevel>();
            firstLevelList = dao.getFirstLevelDir(pager);
            foreach (var item in firstLevelList)
            {
                FirstLevelDirModel demo = entityToModel(item);
                resultList.Add(demo);
            }
            resultPager.data = resultList;
            resultPager.page = pager.page;
            //resultPager.recTotal = 89;
            resultPager.recTotal = dao.getFirstLevelDirCount(pager.data);
            return resultPager;
        }
        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Result<FirstLevelDirModel> addFirstLevelDir(FirstLevelDirModel model)
        {
            FirstLevelDirDAO dao = new FirstLevelDirDAO();
            Result<FirstLevelDirModel> result = new Result<FirstLevelDirModel>();
            if (dao.addFirstLevelDir(model)!=1)
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
        public Result<FirstLevelDirModel> updateFirstLevelDir(FirstLevelDirModel model)
        {
            FirstLevelDirDAO dao = new FirstLevelDirDAO();
            Result<FirstLevelDirModel> result = new Result<FirstLevelDirModel>();
            if (dao.updateFirstLevelDir(model)!=1)
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
        public Result<FirstLevelDirModel> deleteFirstLevelDir(FirstLevelDirModel model)
        {
            FirstLevelDirDAO dao = new FirstLevelDirDAO();
            Result<FirstLevelDirModel> result = new Result<FirstLevelDirModel>();
            if (dao.deleteFirstlevelDir(model) != 1)
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
        public FirstLevelDirModel entityToModel(firstLevel model)
        {
            FirstLevelDirModel entity = new FirstLevelDirModel();
            if (model != null)
            {
                entity.id = model.id;
                entity.orderId = model.orderId;
                entity.createdBy = model.createdBy;
                entity.createdDate = model.createdDate;
                entity.lastUpdatedBy = model.lastUpdatedBy;
                entity.lastUpdatedDate = model.lastUpdatedDate;
                entity.content = model.midContent;
            }
            return entity;
        }
        #endregion
    }
}