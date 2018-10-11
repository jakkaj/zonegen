using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using ZoneModel.Services.Contracts;

namespace ZoneModel.Tests.Tests
{
    [TestClass]
    public class TemplateInitialiserTests : TestBase
    {
        [TestMethod]
        public async Task InitProjectTest()
        {
            var group = "gro";
            var region = "australiasoutheast";
            var env = "devtest";
            var rootDir = Path.Join(Directory.GetCurrentDirectory(), "scratch");
            var initialiser = Resolve<ITemplateInitialiser>();
            var actualRootDir = initialiser.CreateDirectoryStructure(rootDir, group, region, env);
            Assert.AreEqual(actualRootDir, rootDir);
            Assert.IsTrue(Directory.Exists(rootDir));
            Assert.IsTrue(Directory.Exists(Path.Combine(rootDir, "templates", group, region, env)));
            await initialiser.WriteConfigFile();
            Assert.IsTrue(File.Exists(Path.Combine(rootDir, "templates", group, "config.yaml")));
            await initialiser.WriteRuleFiles();
            Assert.IsTrue(File.Exists(Path.Combine(rootDir, "templates", group, region, env, "rule", "access-backend.yaml")));
            Assert.IsTrue(File.Exists(Path.Combine(rootDir, "templates", group, region, env, "rule", "open-internet.yaml")));
            await initialiser.WriteZonesFile();
            Assert.IsTrue(File.Exists(Path.Combine(rootDir, "templates", group, region, env, "zones.yaml")));

            // cleanup
            Directory.Delete(rootDir, true);
        }
    }
}
