using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using _123TribeFrameworker.DAO;
using _123TribeFrameworker.Entity;
using _123TribeFrameworker.Infrastructrue;
using _123TribeFrameworker.Models.QueryModel;
using Unity.Attributes;

namespace _123TribeFrameworker.Services.Layer
{
    public class MaterialInfoService : IMaterialInfoService
    {
        [Dependency]
        public IMaterialInfoDAO dao { get; set; }
        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Result<MaterialInfo>> addAsync(MaterialInfo model)
        {
            Result<MaterialInfo> result = new Result<MaterialInfo>();
            MaterialInfoQuery condition = new MaterialInfoQuery() { materialName = model.materialName,mat_size=model.mat_size };
            var isExist = dao.searchCountByCondition(condition);
            if (isExist > 0)
            {
                result.addError("产品已存在");
            }
            else
            {
                model.findIndx = BeanHelper.getFistCharUpper(model.materialName);
                var resultDAO = await dao.add(model);
                if (!resultDAO.result)
                {
                    result.addError(resultDAO.message);
                }
            }
            return result;
        }
        /// <summary>
        /// 根据条件查询
        /// </summary>
        /// <param name="pager"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public Pager<List<MaterialInfo>> searchByCondition(Pager<List<MaterialInfo>> pager, MaterialInfoQuery condition)
        {
            Task<List<MaterialInfo>> task = Task.Factory.StartNew(() => dao.searchByCondition(pager, condition));
            pager.data = task.Result;
            Task<int> countTask = Task.Factory.StartNew(() => dao.searchCountByCondition(condition));
            pager.recTotal = countTask.Result;
            return pager;
        }
        /// <summary>
        /// 根据id查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<MaterialInfo> searchByid(int id)
        {
            MaterialInfo model = await dao.searchById(id);
            if (model == null)
            {
                model = new MaterialInfo();
            }
            return model;
        }
        /// <summary>
        /// 根据id删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Result<int>> deleteByIdAsync(int id)
        {
            var resultDAO = await dao.deleteById(id);
            return resultDAO;

        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Result<int>> update(MaterialInfo model)
        {
            model.findIndx = BeanHelper.getFistCharUpper(model.materialName);
            return await dao.update(model);
        }
        public async Task<List<MaterialInfo>> searchListByCondition(MaterialInfoQuery condition)
        {
            Task<List<MaterialInfo>> result =  Task.Factory.StartNew(() => dao.searchListByCondition(condition));
            return  await result;
        }
    }
}