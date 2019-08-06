﻿using DotNetGitLabWebHook.Model;
using DotNetGitLabWebHookToMatterMost.Model;
using Microsoft.Extensions.Configuration;

namespace DotNetGitLabWebHookToMatterMost.Business
{
    public class GitLabMRCheckerFlow
    {
        // todo 处理并发
        // todo 使用消息队列

        public static void AddToCheck(GitLabMergeRequest gitLabMergeRequest)
        {
            var notify = new Notify(Configuration);
            notify.NotifyMatterMost(gitLabMergeRequest);
        }

        public static IConfiguration Configuration { get; set; }
    }
}