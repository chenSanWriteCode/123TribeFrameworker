using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _123TribeFrameworker.DAO;
using _123TribeFrameworker.Entity;
using _123TribeFrameworker.Infrastructrue;
using Unity.Attributes;

namespace _123TribeFrameworker.Services.Layer
{
    public class OutRecordsLayer : IOutRecordsLayer
    {
        [Dependency]
        public IMaterialInfoDAO dao { get; set; }
        public int getRecordsCount()
        {
            LayerDbContext context = new LayerDbContext();
            MaterialInfo model1 = new MaterialInfo();
            MaterialInfo model2 = new MaterialInfo();
            model1.material = "234";
            BeanHelper.CopyBean(ref model2,model1);
            var result  = context.orderInfo.ToList();
            var result1 = context.orderDetailInfo.ToList();
            var result2 = dao.deleteById(0);
            return 10; 
        }
    }
}