using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetGitLabWebHookToMatterMost.Model;
using Microsoft.Extensions.Configuration;

namespace DotNetGitLabWebHookToMatterMost.Business
{
    public class Notify
    {
        /// <inheritdoc />
        public Notify(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; set; }
        public void NotifyMatterMost(GitLabMergeRequest gitLabMergeRequest)
        {
            var state = gitLabMergeRequest.RawProperty.object_attributes.state;
           
            // 如果是关闭了，那么不处理
            if (state == "closed")
            {
                return;
            }

            //var action = gitLabMergeRequest.RawProperty.object_attributes.action;
            // action 有三个值 update open reopen
           
            var assignees = gitLabMergeRequest.RawProperty.assignees;
            var assigneeList = new List<string> { gitLabMergeRequest.RawProperty.object_attributes.assignee?.username };
            if (assignees != null)
            {
                assigneeList.AddRange(assignees.Select(temp => temp.username));
            }

            var assigneeUsername = string.Join(' ',
                assigneeList.Where(temp => temp != null).Distinct().Select(temp => $"@{temp}"));

            var text = $"{assigneeUsername} 快来处理代码 [{gitLabMergeRequest.CommonProperty.Title}]({gitLabMergeRequest.CommonProperty.MergeRequestUrl})";
            Task.Run(() =>
            {
                var matterMost = new MatterMost(Configuration["MatterMostCodeReviewUrl"]);
                matterMost.SendText(text);
            });
        }
    }
}