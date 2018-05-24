using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _123TribeFrameworker;
using _123TribeFrameworker.Entity;
using _123TribeFrameworker.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace _123TribeFrameworker.Infrastructrue
{
    public static class IdentityHelper
    {
        /// <summary>
        /// 根据userId获取userName
        /// </summary>
        /// <param name="html"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static MvcHtmlString getUserName(this HtmlHelper html, string id)
        {
            ApplicationUserManager mgr = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            return new MvcHtmlString(mgr.FindByIdAsync(id).Result.UserName);
        }
        /// <summary>
        /// 根据roleId 获取role
        /// </summary>
        /// <param name="html"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static AplicationRole getRole(this HtmlHelper html,string id)
        {
            ApplicationRoleManager roleManger = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationRoleManager>();
            return roleManger.FindByIdAsync(id).Result;
        }
        /// <summary>
        /// 根据menuID ,level获取menuName
        /// </summary>
        /// <param name="html"></param>
        /// <param name="menuId"></param>
        /// <param name="menuLevel"></param>
        /// <returns></returns>
        public static MvcHtmlString getMenuName(this HtmlHelper html,int menuId,int menuLevel)
        {
            string menuName = "";
            practiceEntities entities = new practiceEntities();
            switch (menuLevel)
            {
                case 1:
                    menuName=entities.FirstLevel.Where(x => x.id == menuId).Select(x => x.midContent).First();
                    break;
                case 2:
                    menuName = entities.SecondLevel.Where(x => x.id == menuId).Select(x => x.title).First();
                    break;
                case 3:
                    menuName = entities.ThirdLevel.Where(x => x.id == menuId).Select(x => x.title).First();
                    break;
                default:
                    break;
            }
            return new MvcHtmlString(menuName);
        }

    }
}