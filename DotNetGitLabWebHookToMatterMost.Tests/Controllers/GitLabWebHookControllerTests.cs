using Microsoft.VisualStudio.TestTools.UnitTesting;
using DotNetGitLabWebHookToMatterMost.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Text;
using DotNetGitLabWebHook;
using DotNetGitLabWebHookToMatterMost.Business;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Moq;

namespace DotNetGitLabWebHookToMatterMost.Controllers.Tests
{
    [TestClass()]
    public class GitLabWebHookControllerTests
    {
        [TestMethod()]
        public void MergeRequestTest()
        {
            var hostBuilder = CreateHostBuilder(new string[0]);
            var build = hostBuilder.Build();
            var serviceProvider = build.Services;
            
            using (var scope = serviceProvider.CreateScope())
            {
                var gitLabMrCheckerFlow = scope.ServiceProvider.GetService<GitLabMRCheckerFlow>();

                var gitLabWebHookController = new GitLabWebHookController(gitLabMrCheckerFlow);
                gitLabWebHookController.MergeRequest(TestMRJson.GetObject());
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseUrls("http://0.0.0.0:6000");
                });
    }

    public class TestMRJson
    {
        public static string GetObject()
        {
            return @"{""object_kind"":""merge_request"",""event_type"":""merge_request"",""user"":{""name"":""lindexi"",""username"":""lindexi_gd"",""avatar_url"":""https://secure.gravatar.com/avatar/28392f457e177c01d60c15d75a9c5682?s=80\u0026d=identicon""},""project"":{""id"":16638644,""name"":""BohewhaigereciHalulerhafu"",""description"":"""",""web_url"":""https://gitlab.com/lindexi_gd/bohewhaigerecihalulerhafu"",""avatar_url"":null,""git_ssh_url"":""git@gitlab.com:lindexi_gd/bohewhaigerecihalulerhafu.git"",""git_http_url"":""https://gitlab.com/lindexi_gd/bohewhaigerecihalulerhafu.git"",""namespace"":""lindexi"",""visibility_level"":20,""path_with_namespace"":""lindexi_gd/bohewhaigerecihalulerhafu"",""default_branch"":""master"",""ci_config_path"":null,""homepage"":""https://gitlab.com/lindexi_gd/bohewhaigerecihalulerhafu"",""url"":""git@gitlab.com:lindexi_gd/bohewhaigerecihalulerhafu.git"",""ssh_url"":""git@gitlab.com:lindexi_gd/bohewhaigerecihalulerhafu.git"",""http_url"":""https://gitlab.com/lindexi_gd/bohewhaigerecihalulerhafu.git""},""object_attributes"":{""assignee_id"":2867711,""author_id"":2867711,""created_at"":""2020-02-01T02:08:16.881Z"",""description"":"""",""head_pipeline_id"":null,""id"":48778767,""iid"":1,""last_edited_at"":null,""last_edited_by_id"":null,""merge_commit_sha"":null,""merge_error"":null,""merge_params"":{""force_remove_source_branch"":""1""},""merge_status"":""can_be_merged"",""merge_user_id"":null,""merge_when_pipeline_succeeds"":false,""milestone_id"":null,""source_branch"":""t/lindexi/LairhanejiFearjeleajem"",""source_project_id"":16638644,""state_id"":1,""target_branch"":""master"",""target_project_id"":16638644,""time_estimate"":0,""title"":""HeakerfaiyiFulurkelwar"",""updated_at"":""2020-02-01T02:08:16.881Z"",""updated_by_id"":null,""url"":""https://gitlab.com/lindexi_gd/bohewhaigerecihalulerhafu/-/merge_requests/1"",""source"":{""id"":16638644,""name"":""BohewhaigereciHalulerhafu"",""description"":"""",""web_url"":""https://gitlab.com/lindexi_gd/bohewhaigerecihalulerhafu"",""avatar_url"":null,""git_ssh_url"":""git@gitlab.com:lindexi_gd/bohewhaigerecihalulerhafu.git"",""git_http_url"":""https://gitlab.com/lindexi_gd/bohewhaigerecihalulerhafu.git"",""namespace"":""lindexi"",""visibility_level"":20,""path_with_namespace"":""lindexi_gd/bohewhaigerecihalulerhafu"",""default_branch"":""master"",""ci_config_path"":null,""homepage"":""https://gitlab.com/lindexi_gd/bohewhaigerecihalulerhafu"",""url"":""git@gitlab.com:lindexi_gd/bohewhaigerecihalulerhafu.git"",""ssh_url"":""git@gitlab.com:lindexi_gd/bohewhaigerecihalulerhafu.git"",""http_url"":""https://gitlab.com/lindexi_gd/bohewhaigerecihalulerhafu.git""},""target"":{""id"":16638644,""name"":""BohewhaigereciHalulerhafu"",""description"":"""",""web_url"":""https://gitlab.com/lindexi_gd/bohewhaigerecihalulerhafu"",""avatar_url"":null,""git_ssh_url"":""git@gitlab.com:lindexi_gd/bohewhaigerecihalulerhafu.git"",""git_http_url"":""https://gitlab.com/lindexi_gd/bohewhaigerecihalulerhafu.git"",""namespace"":""lindexi"",""visibility_level"":20,""path_with_namespace"":""lindexi_gd/bohewhaigerecihalulerhafu"",""default_branch"":""master"",""ci_config_path"":null,""homepage"":""https://gitlab.com/lindexi_gd/bohewhaigerecihalulerhafu"",""url"":""git@gitlab.com:lindexi_gd/bohewhaigerecihalulerhafu.git"",""ssh_url"":""git@gitlab.com:lindexi_gd/bohewhaigerecihalulerhafu.git"",""http_url"":""https://gitlab.com/lindexi_gd/bohewhaigerecihalulerhafu.git""},""last_commit"":{""id"":""a9570043488eaafea1e75f67f41c7ce2126a26ee"",""message"":""HeakerfaiyiFulurkelwar\n"",""timestamp"":""2020-02-01T10:04:26+08:00"",""url"":""https://gitlab.com/lindexi_gd/bohewhaigerecihalulerhafu/commit/a9570043488eaafea1e75f67f41c7ce2126a26ee"",""author"":{""name"":""lindexi"",""email"":""lindexi_gd@163.com""}},""work_in_progress"":false,""total_time_spent"":0,""human_total_time_spent"":null,""human_time_estimate"":null,""assignee_ids"":[2867711],""state"":""opened""},""labels"":[],""changes"":{""assignees"":{""previous"":[],""current"":[{""name"":""lindexi"",""username"":""lindexi_gd"",""avatar_url"":""https://secure.gravatar.com/avatar/28392f457e177c01d60c15d75a9c5682?s=80\u0026d=identicon""}]},""total_time_spent"":{""previous"":null,""current"":0}},""repository"":{""name"":""BohewhaigereciHalulerhafu"",""url"":""git@gitlab.com:lindexi_gd/bohewhaigerecihalulerhafu.git"",""description"":"""",""homepage"":""https://gitlab.com/lindexi_gd/bohewhaigerecihalulerhafu""},""assignees"":[{""name"":""lindexi"",""username"":""lindexi_gd"",""avatar_url"":""https://secure.gravatar.com/avatar/28392f457e177c01d60c15d75a9c5682?s=80\u0026d=identicon""}]}";
        }
    }
}