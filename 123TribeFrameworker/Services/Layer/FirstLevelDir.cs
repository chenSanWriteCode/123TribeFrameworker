using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _123TribeFrameworker.DAO.DirDAO;
using _123TribeFrameworker.Entity;
using _123TribeFrameworker.Models;

namespace _123TribeFrameworker.Services.Layer
{
    public class FirstLevelDir 
    {
        /// <summary>
        /// 通过条件进行查询
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<FirstLevelDirModel> getFirstLevelDir(FirstLevelDirModel model)
        {
            FirstLevelDIrDAO dao = new FirstLevelDIrDAO();
            List<FirstLevelDirModel> returnList = new List<FirstLevelDirModel>();
            List<firstLevel> firstLevelList = new List<firstLevel>();
            firstLevelList = dao.getFirstLevelDir(model);
            foreach (var item in firstLevelList)
            {
                FirstLevelDirModel demo = entityToModel(item);
                returnList.Add(demo);
            }
            return returnList;
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