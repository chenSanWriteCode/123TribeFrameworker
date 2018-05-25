using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using _123TribeFrameworker.Entity;

namespace _123TribeFrameworker.DAO.BussinessDAO
{
    public class MaterialInfoDAO : IMaterialInfoDAO
    {
        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public async Task<int> add(MaterialInfo t)
        {
            LayerDbContext context = new LayerDbContext();
            context.materialInfos.Add(t);
            var result = await context.SaveChangesAsync();
            return result;
        }
        /// <summary>
        /// 通过id删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<int> deleteById(int id)
        {
            LayerDbContext context = new LayerDbContext();
            var model = context.materialInfos.Where(x => x.id == id).ToList();
            if (model.Count > 0)
            {
                context.materialInfos.Remove(model[0]);
            }
            var result = await context.SaveChangesAsync();
            return result;
        }
        /// <summary>
        /// 获取所有物料
        /// </summary>
        /// <returns></returns>
        public List<MaterialInfo> searchAllMaterialInfo()
        {
            LayerDbContext context = new LayerDbContext();
            return context.materialInfos.ToList();
        }

        /// <summary>
        /// 根据条件查询
        /// </summary>
        /// <param name="pager"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public List<MaterialInfo> searchByCondition(Pager<List<MaterialInfo>> pager, MaterialInfo t)
        {
            LayerDbContext context = new LayerDbContext();
            int start=(pager.page - 1)*pager.recPerPage;
            var result = context.materialInfos.Where(x => x.id > 0);
            result = string.IsNullOrEmpty(t.material) ? result : result.Where(x => x.material.Contains(t.material));
            result = string.IsNullOrEmpty(t.materialName) ? result : result.Where(x => x.materialName.Contains(t.materialName));
            result = string.IsNullOrEmpty(t.mat_color) ? result : result.Where(x => x.mat_color == t.mat_color);
            result = string.IsNullOrEmpty(t.mat_size) ? result : result.Where(x => x.mat_size == t.mat_size);
            result = string.IsNullOrEmpty(t.mat_type) ? result : result.Where(x => x.mat_type == t.mat_type);
            result = string.IsNullOrEmpty(t.alias) ? result : result.Where(x => x.alias.Contains(t.alias));
            result = string.IsNullOrEmpty(t.remark) ? result : result.Where(x => x.remark.Contains(t.remark));
            result = result.OrderBy(x=>x.material);
            result = result.Skip(start).Take(pager.recPerPage);
            return result.ToList();
        }
        /// <summary>
        /// 根据id查询记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<MaterialInfo> searchById(int id)
        {
            LayerDbContext context = new LayerDbContext();
            var model = await context.materialInfos.FindAsync(id);
            return model;
        }
        /// <summary>
        /// 根据条件查询记录数
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public int searchCountByCondition(MaterialInfo t)
        {
            LayerDbContext context = new LayerDbContext();
            var result = context.materialInfos.Where(x => x.id > 0);
            result = string.IsNullOrEmpty(t.material) ? result : result.Where(x => x.material.Contains(t.material));
            result = string.IsNullOrEmpty(t.materialName) ? result : result.Where(x => x.materialName.Contains(t.materialName));
            result = string.IsNullOrEmpty(t.mat_color) ? result : result.Where(x => x.mat_color == t.mat_color);
            result = string.IsNullOrEmpty(t.mat_size) ? result : result.Where(x => x.mat_size == t.mat_size);
            result = string.IsNullOrEmpty(t.mat_type) ? result : result.Where(x => x.mat_type == t.mat_type);
            result = string.IsNullOrEmpty(t.mat_type) ? result : result.Where(x => x.mat_type == t.mat_type);
            result = string.IsNullOrEmpty(t.alias) ? result : result.Where(x => x.alias.Contains(t.alias));
            result = string.IsNullOrEmpty(t.remark) ? result : result.Where(x => x.remark.Contains(t.remark));
            return result.Count();
        }

        public async Task<int> update(MaterialInfo t)
        {
            LayerDbContext context = new LayerDbContext();
            MaterialInfo model = await context.materialInfos.FindAsync(t.id); ;
            if (model != null)
            {
                model.alias = t.alias;
                model.material = t.material;
                model.materialName = t.materialName;
                model.mat_color = t.mat_color;
                model.mat_size = t.mat_size;
                model.mat_type = t.mat_type;
                model.referencePriceIn = t.referencePriceIn;
                model.referencePriceOut = t.referencePriceOut;
                model.remark = t.remark;
                model.weight = t.weight;
            }
            var result = await context.SaveChangesAsync();
            return result;
        }
        /// <summary>
        /// 根据物料查询记录
        /// </summary>
        /// <param name="material"></param>
        /// <returns></returns>
        //public async Task<MaterialInfo> searchByMaterial(string material)
        //{
        //    LayerDbContext context = new LayerDbContext();
        //    var model = await context.materialInfos.FindAsync(material);
        //    return model;
        //}
    }
}