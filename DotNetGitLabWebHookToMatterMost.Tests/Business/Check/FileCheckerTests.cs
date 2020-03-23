using Microsoft.VisualStudio.TestTools.UnitTesting;
using DotNetGitLabWebHookToMatterMost.Business.Check;
using System;
using System.Collections.Generic;
using System.Text;
using DotNetGitLabWebHook;
using DotNetGitLabWebHookToMatterMost.Controllers.Tests;
using DotNetGitLabWebHookToMatterMost.Model;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace DotNetGitLabWebHookToMatterMost.Business.Check.Tests
{
    [TestClass()]
    public class FileCheckerTests
    {
        [TestMethod()]
        public void CheckTest()
        {
            var rootobject =
                JsonConvert.DeserializeObject<GitLabMergeRequest.Rootobject>(TestMRJson.GetObject());

            var hostBuilder = Program.CreateHostBuilder(new string[0]);
            var build = hostBuilder.Build();
            var serviceProvider = build.Services;

            using (var scope = serviceProvider.CreateScope())
            {
                var gitLabMergeRequestProvider = scope.ServiceProvider.GetService<GitLabMergeRequestProvider>();
                var repoManager = scope.ServiceProvider.GetService<RepoManager>();

                var gitLabMergeRequest = gitLabMergeRequestProvider.ParseGitLabMergeRequest(rootobject);
                var fileChecker = new FileChecker(repoManager);

                fileChecker.Check(gitLabMergeRequest);
            }
        }
    }
}