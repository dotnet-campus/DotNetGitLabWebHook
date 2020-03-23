using Microsoft.VisualStudio.TestTools.UnitTesting;
using DotNetGitLabWebHookToMatterMost.Business.Check;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using Moq;
using MSTest.Extensions.Contracts;

namespace DotNetGitLabWebHookToMatterMost.Business.Check.Tests
{
    [TestClass()]
    public class RepoManagerTests
    {
        public RepoManagerTests()
        {
            var mock = new Mock<IConfiguration>();
            mock.Setup(configuration => configuration[RepoManager.RepoFolderConfiguration])
                .Returns(RepoManager.RepoFolderConfiguration);

            MockConfiguration = mock.Object;
        }

        [TestCleanup]
        public void Clean()
        {
            Directory.Delete(RepoManager.RepoFolderConfiguration, true);
            Directory.Delete(RepoManager.RepoFolderConfiguration, true);
        }

        private IConfiguration MockConfiguration { get; }

        [ContractTestCase]
        public void GetRepoTest()
        {
            "测试拉取一个存在的项目，可以下载到本地".Test(() =>
            {
                // Arrange
                var repoManager = new RepoManager(MockConfiguration);

                // Action
                var directory = repoManager.GetRepo("https://gitlab.com/lindexi_gd/bohewhaigerecihalulerhafu.git",
                    "BohewhaigereciHalulerhafu ");
                // Assert
                Assert.AreEqual(true, Directory.Exists(directory.FullName));

            });
        }
    }
}