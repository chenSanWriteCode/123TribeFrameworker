using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _123TribeFrameworker.DAO.DirDAO;
using _123TribeFrameworker.Entity;
using _123TribeFrameworker.Models.DirModels;

namespace _123TribeFrameworker.Services.Layer
{
    public class SecondLevelDir
    {
        /// <summary>
        /// 通过条件进行查询
        /// </summary>
        /// <param name="pager"></param>
        /// <returns></returns>
        public Pager<List<SecondLevelDirModel>> getSecondLevelDir(Pager<SecondLevelDirModel> pager)
        {
            SecondLevelDirDAO dao = new SecondLevelDirDAO();

            Pager<List<SecondLevelDirModel>> resultPager = new Pager<List<SecondLevelDirModel>>();
            List<SecondLevelDirModel> resultList = new List<SecondLevelDirModel>();
            List<SecondLevel> SecondLevelList = new List<SecondLevel>();
            SecondLevelList = dao.getSecondLevelDir(pager);
            foreach (var item in SecondLevelList)
            {
                SecondLevelDirModel demo = entityToModel(item);
                resultList.Add(demo);
            }
            resultPager.data = resultList;
            resultPager.page = pager.page;
            //resultPager.recTotal = 89;
            resultPager.recTotal = dao.getSecondLevelDirCount(pager.data);
            return resultPager;
        }

        /// <summary>
        /// 获取二级菜单字典 id,content
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, string> getSecondLevelDirDict()
        {
            SecondLevelDirDAO dao = new SecondLevelDirDAO();
            List<SecondLevel> list = dao.getSecondLevelDirList();
            Dictionary<int, string> secondDict = new Dictionary<int, string>();
            foreach (var item in list)
            {
                secondDict.Add(item.id, item.title);
            }
            return secondDict;
        }
        /// <summary>
        /// 根据id获取一条二级菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SecondLevelDirModel getSingleSecondDir(int id)
        {
            SecondLevelDirDAO dao = new SecondLevelDirDAO();
            SecondLevel entity = dao.getSingleSecondDir(id);
            return entityToModel(entity);
        }
        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Result<SecondLevelDirModel> addSecondLevelDir(SecondLevelDirModel model)
        {
            SecondLevelDirDAO dao = new SecondLevelDirDAO();
            Result<SecondLevelDirModel> result = new Result<SecondLevelDirModel>();
            if (dao.addSecondLevelDir(model) != 1)
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
        public Result<SecondLevelDirModel> updateSecondLevelDir(SecondLevelDirModel model)
        {
            SecondLevelDirDAO dao = new SecondLevelDirDAO();
            Result<SecondLevelDirModel> result = new Result<SecondLevelDirModel>();
            if (dao.updateSecondLevelDir(model) != 1)
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
        public Result<SecondLevelDirModel> deleteSecondLevelDir(int id)
        {
            SecondLevelDirDAO dao = new SecondLevelDirDAO();
            Result<SecondLevelDirModel> result = new Result<SecondLevelDirModel>();
            if (dao.deleteSecondlevelDir(id) != 1)
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
        public SecondLevelDirModel entityToModel(SecondLevel model)
        {
            SecondLevelDirModel entity = new SecondLevelDirModel();
            if (model != null)
            {
                entity.firstLevelName = model.FirstLevel.midContent;
                entity.id = model.id;
                entity.orderId = model.orderId;
                entity.createdBy = model.createdBy;
                entity.createdDate = model.createdDate;
                entity.lastUpdatedBy = model.lastUpdateBy;
                entity.lastUpdatedDate = model.lastUpdatedDate;
                entity.title = model.title;
                entity.firstLevelID = model.firstLevelID.Value;
            }
            return entity;
        }


        #endregion
    }
}