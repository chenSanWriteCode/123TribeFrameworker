using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _123TribeFrameworker.DAO.DirDAO;
using _123TribeFrameworker.Entity;
using _123TribeFrameworker.Models.DirModels;
using Unity.Attributes;

namespace _123TribeFrameworker.Services.Layer
{
    public class FirstLevelDirServiceImpl : IFirstLevelDirService
    {
        [Dependency]
        public IRoleMenuLayerService dirService { get; set; }
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
            List<FirstLevel> firstLevelList = new List<FirstLevel>();
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
        /// 获取主菜单字典 id,content
        /// </summary>
        /// <returns></returns>
        public Dictionary<int,string> getFirstLevelDirDict()
        {
            FirstLevelDirDAO dao = new FirstLevelDirDAO();
            List<FirstLevel> list = dao.getFirstLevelDirList();
            Dictionary<int, string> firstDict = new Dictionary<int, string>();
            foreach (var item in list)
            {
                firstDict.Add(item.id, item.midContent);
            }
            return firstDict;
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
                result.success = false;
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
                result.success = false;
            }
            return result;
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Result<FirstLevelDirModel> deleteFirstLevelDir(int id)
        {
            FirstLevelDirDAO dao = new FirstLevelDirDAO();
            Result<FirstLevelDirModel> result = new Result<FirstLevelDirModel>();
            if (dao.deleteFirstlevelDir(id) != 1)
            {
                result.success = false;
            }
            else
            {
                dirService.deleteMenuAsync(id);
            }
            return result;
        }
        #region  工具
        /// <summary>
        /// 后端entity转前端model
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public FirstLevelDirModel entityToModel(FirstLevel model)
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