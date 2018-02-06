using System;
using _123TribeFrameworker.Services.Layer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace _123TribeFrameworker.Tests.Layer
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void searchSecondDirTest()
        {
            DirLayer dirLayer = new DirLayer();
            dirLayer.searchSecondDir(1);
        }
    }
}
