// <copyright file="RoleMenuDAOTest.cs" company="Microsoft">版权所有(C) Microsoft 2018</copyright>
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _123TribeFrameworker.DAO.DirDAO;
using _123TribeFrameworker.Entity;

namespace _123TribeFrameworker.DAO.DirDAO.Tests
{
    /// <summary>此类包含 RoleMenuDAO 的参数化单元测试</summary>
    [PexClass(typeof(RoleMenuDAO))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [TestClass]
    public partial class RoleMenuDAOTest
    {
        /// <summary>测试 addRoleMenuRangeAsync(List`1&lt;RoleMenu&gt;) 的存根</summary>
        [PexMethod]
        public Task<Result<RoleMenu>> addRoleMenuRangeAsyncTest(
            [PexAssumeUnderTest]RoleMenuDAO target,
            List<RoleMenu> menuList
        )
        {
            RoleMenu model = new RoleMenu();
            model.id = 2;
            model.menuId = 1;
            model.menuLevel = 1;
            model.roleId = "e749f1d9-34af-4892-ac67-9078e5bbb8ae";
            menuList.Add(model);
            Task<Result<RoleMenu>> result = target.addRoleMenuRangeAsync(menuList);
            return result;
            // TODO: 将断言添加到 方法 RoleMenuDAOTest.addRoleMenuRangeAsyncTest(RoleMenuDAO, List`1<RoleMenu>)
        }
    }
}
