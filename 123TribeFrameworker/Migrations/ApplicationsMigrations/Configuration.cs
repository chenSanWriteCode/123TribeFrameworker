namespace _123TribeFrameworker.migrations.ApplicationsMigrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using CommonTools;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using Models.DirModels;
    using Services.Layer;

    internal sealed class Configuration : DbMigrationsConfiguration<_123TribeFrameworker.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            MigrationsDirectory = @"migrations\ApplicationsMigrations";
        }

        protected override void Seed(_123TribeFrameworker.Models.ApplicationDbContext context)
        {
            //初始化角色 要有一个administrator角色
            //初始化用户 要有一个admin用户 密码 hai123456
            //给admin配置administrator角色
            //给administrator配置所有菜单权限
            ApplicationRoleManager roleManager = new ApplicationRoleManager(new RoleStore<AplicationRole>(context));
            ApplicationUserManager userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            string roleName = "administrator";
            ApplicationUser user = new ApplicationUser() { UserName = "admin" };
            if (!roleManager.RoleExists(roleName))
            {
                roleManager.Create(new AplicationRole(roleName));
            }
            if (userManager.FindByName("admin")==null)
            {
                userManager.Create(user, "hai123456");
                user = userManager.FindByName("admin");
            }
            if (!userManager.IsInRole(user.Id,roleName))
            {
                userManager.AddToRole(user.Id, roleName);
            }

            RoleMenuLayerImpl layer = new RoleMenuLayerImpl();
            AplicationRole role = roleManager.FindByName(roleName);
            var firstDirs = layer.searchRoleMenusNotInRoleId(role.Id, DirLevel.FirstLevel);
            var secondDirs = layer.searchRoleMenusNotInRoleId(role.Id, DirLevel.SecondLevel);
            if (firstDirs != null && firstDirs.Count() > 0)
            {
                var resultF  = layer.addRoleMenuRangeAsync(role.Id, DirLevel.FirstLevel, firstDirs.Select(x=>x.menuId).ToArray());
            }
            if (secondDirs != null && secondDirs.Count() > 0)
            {
                var resultS = layer.addRoleMenuRangeAsync(role.Id, DirLevel.SecondLevel, secondDirs.Select(x=>x.menuId).ToArray());
            }
        }

    }
}
