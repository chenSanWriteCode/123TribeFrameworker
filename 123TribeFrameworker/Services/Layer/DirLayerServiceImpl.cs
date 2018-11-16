using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using _123TribeFrameworker.Entity;
using _123TribeFrameworker.Models;
using _123TribeFrameworker.Models.DirModels;
using Newtonsoft.Json;

namespace _123TribeFrameworker.Services.Layer
{
    public class DirLayerServiceImpl: IDirLayerService
    {
        
        public List<Menu> searchSecondDir(string roleId ,int ID)
        {
            DirDbContext dirContext = new DirDbContext();
            RoleMenuDbContext roleContext = new RoleMenuDbContext();
            var menuIds = roleContext.roleMenus.Where(x => x.roleId == roleId && x.menuLevel == 2).Select(x => x.menuId).ToArray();
            var secondDirs = dirContext.secondLevels.Where(x => x.firstLevelId == ID && menuIds.Contains(x.id) && x.activityFlag == 1).OrderBy(x => x.orderId).ToList(); 
            List<Menu> result = new List<Menu>();
            Menu model = null;
            foreach (var item in secondDirs)
            {
                model = new Menu();
                model.Id = item.id;
                model.open = item.open;
                model.title = item.title;
                model.url = item.url;
                result.Add(model);
            }
            return result;
        }

        public string searchMainDir(string roleId)
        {
            DirDbContext dirContext = new DirDbContext();
            RoleMenuDbContext roleContext = new RoleMenuDbContext();
            var menuIds = roleContext.roleMenus.Where(x => x.roleId == roleId && x.menuLevel==1).Select(x=>x.menuId).ToArray();
            var mainDir = dirContext.firstLevels.Where(x=>menuIds.Contains(x.id) && x.activityFlag==1).OrderBy(x=>x.orderId).ToList();
            StringBuilder htmlStr = new StringBuilder("");
            foreach (var item in mainDir)
            {
                htmlStr.Append(item.beforContent).Append(item.midContent).Append(item.afterContent);
            }
            return htmlStr.ToString();
        }
    }
}