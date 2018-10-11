using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ZoneModel.Tests.Tests
{
    [TestClass]
    public class PathTests
    {
        [TestMethod]
        public void TestPathOffsets()
        {
            var relPath = "somefolder/somethingelse";

            var fullPath = "c:\\temp\\somepath";


            var actualRel = Path.GetFullPath(relPath);

            var actualFull = Path.GetFullPath(fullPath);


        }
    }
}
