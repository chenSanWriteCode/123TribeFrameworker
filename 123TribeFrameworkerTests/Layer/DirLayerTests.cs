using Microsoft.VisualStudio.TestTools.UnitTesting;
using _123TribeFrameworker.Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _123TribeFrameworker.Layer.Tests
{
    [TestClass()]
    public class DirLayerTests
    {
        [TestMethod()]
        public void searchSecondDirTest()
        {
            DirLayer dirLayer = new DirLayer();
            dirLayer.searchSecondDir();
        }
    }
}