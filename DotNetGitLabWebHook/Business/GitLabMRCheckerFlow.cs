using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DotNetGitLabWebHook.Model;
using Microsoft.Extensions.Configuration;

namespace DotNetGitLabWebHook.Business
{
    public class GitLabMRCheckerFlow
    {
        // todo 处理并发
        // todo 使用消息队列

        public static void AddToCheck(GitLabMergeRequest gitLabMergeRequest)
        {
            var assignees = gitLabMergeRequest.RawProperty.assignees;
            var assigneeList = new List<string> { gitLabMergeRequest.RawProperty.object_attributes.assignee?.username };
            if (assignees != null)
            {
                assigneeList.AddRange(assignees.Select(temp => temp.username));
            }

            var assigneeUsername = string.Join(' ',
                assigneeList.Where(temp => temp != null).Distinct().Select(temp => $"@{temp}"));

            var text = $"{assigneeUsername} 快来审查代码 {gitLabMergeRequest.CommonProperty.MergeRequestUrl}";
            Task.Run(() =>
            {
                var matterMost = new MatterMost(Configuration["MatterMostCodeReviewUrl"]);
                matterMost.SendText(text);
            });
        }

        public static IConfiguration Configuration { get; set; }
    }

    public class MatterMost
    {
        private readonly string _url;

        /// <inheritdoc />
        public MatterMost(string url)
        {
            _url = url;
        }

        public void SendText(string text)
        {
            var httpClient = new HttpClient();
            StringContent content = new StringContent("{\"text\": \"" 
                                                      + text +
                                                      "\"}", Encoding.UTF8, "application/json");
            httpClient.PostAsync(_url, content);
        }
    }
}