using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using _123TribeFrameworker.DAO;
using _123TribeFrameworker.DAO.DirDAO;
using _123TribeFrameworker.Entity;
using _123TribeFrameworker.Models.DirModels;
using _123TribeFrameworker.Models.QueryModel;
using Unity.Attributes;

namespace _123TribeFrameworker.Services.Layer
{
    public class SecondLevelDirServiceImpl : ISecondLevelDirService
    {
        [Dependency]
        public ISecondLevelDirDAO dao { get; set; }
        /// <summary>
        /// 通过条件进行查询
        /// </summary>
        /// <param name="pager"></param>
        /// <returns></returns>
        public async Task<Result<Pager<List<SecondLevel>>>> getSecondLevelDir(Pager<List<SecondLevel>> pager, SecondMenuQuery condition)
        {
            Result<Pager<List<SecondLevel>>> result = new Result<Pager<List<SecondLevel>>>();
            Result<List<SecondLevel>> result_data = await Task.Factory.StartNew(() => dao.getSecondLevelDir(pager, condition));
            if (result_data.success)
            {
                pager.data = result_data.data;
                pager.recTotal = await Task.Factory.StartNew(()=> dao.getSecondLevelDirCount(condition));
            }
            else
            {
                result.addError(result_data.message);
            }
            return result;
        }

        /// <summary>
        /// 根据id获取一条二级菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Result<SecondLevel>> getSingleSecondDir(int id)
        {
            return await Task.Factory.StartNew(() => dao.getSingleSecondDir(id));
        }
        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Result<SecondLevel>> addSecondLevelDir(SecondLevelDirModel model, string userName)
        {
            SecondLevel data = new SecondLevel
            {
                createdBy = userName,
                firstLevelId = model.firstLevelID,
                open = model.open,
                title = model.title,
                url = model.url,
                orderId = model.orderId
            };
            return await dao.addSecondLevelDir(data);
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Result<SecondLevel>> updateSecondLevelDir(SecondLevelDirModel model, string userName)
        {
            SecondLevel data = new SecondLevel
            {
                id=model.id,
                lastUpdatedBy = userName,
                lastUpdatedDate=DateTime.Now,
                firstLevelId = model.firstLevelID,
                open = model.open,
                title = model.title,
                url = model.url,
                orderId = model.orderId
            };
            return await dao.updateSecondLevelDir(data);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Result<int>> deleteSecondLevelDir(int id)
        {
            return await Task.Factory.StartNew(()=> dao.deleteSecondlevelDir(id));
        }
    }
}