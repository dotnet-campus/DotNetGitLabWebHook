using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetGitLabWebHook.Model;
using DotNetGitLabWebHookToMatterMost.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DotNetGitLabWebHook
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var sourceGitSshUrl = "git@gitlab.gz.cvte.cn:lindexi/faljayrarwemwarwhefairberelejeaku.git";
            var sourceBranch = "banelaceajayJechemkile";

            // 最后提交号
            var lastCommitId = "75ca2bf8452061c8b44e6845993ac76c650dad2a";

            var targetGitSshUrl = "git@gitlab.gz.cvte.cn:lindexi/faljayrarwemwarwhefairberelejeaku.git";
            var targetBranch = "master";

            // MR 标题
            var title = "LaihechaliNellabemdo";

            var username = "lindexi";
            var mergeRequestUrl = "https://gitlab.gz.cvte.cn/lindexi/faljayrarwemwarwhefairberelejeaku/merge_requests/1";

            var gitLabMergeRequest = new GitLabMergeRequest
            {
                CommonProperty = new GitLabMergeRequest.MergeRequestProperty(sourceGitSshUrl,
                    sourceBranch,
                    lastCommitId,
                    targetGitSshUrl,
                    targetBranch,
                    title,
                    username,
                    mergeRequestUrl)
            };



            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseUrls("http://0.0.0.0:5006");
                });
    }
}
