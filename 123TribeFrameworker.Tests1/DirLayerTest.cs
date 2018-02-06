// <copyright file="DirLayerTest.cs" company="Microsoft">版权所有(C) Microsoft 2018</copyright>
using System;
using System.Collections.Generic;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _123TribeFrameworker.Entity;
using _123TribeFrameworker.Layer;

namespace _123TribeFrameworker.Layer.Tests
{
    /// <summary>此类包含 DirLayer 的参数化单元测试</summary>
    [PexClass(typeof(DirLayer))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [TestClass]
    public partial class DirLayerTest
    {
        /// <summary>测试 searchSecondDir() 的存根</summary>
        [PexMethod]
        public List<searchSecondDir_Result> searchSecondDirTest([PexAssumeUnderTest]DirLayer target)
        {
            List<searchSecondDir_Result> result = target.searchSecondDir();
            return result;
            // TODO: 将断言添加到 方法 DirLayerTest.searchSecondDirTest(DirLayer)
        }
    }
}
