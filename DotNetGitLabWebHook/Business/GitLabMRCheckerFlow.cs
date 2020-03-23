using DotNetGitLabWebHookToMatterMost.Business.Check;
using DotNetGitLabWebHookToMatterMost.Model;
using Microsoft.Extensions.Configuration;

namespace DotNetGitLabWebHookToMatterMost.Business
{
    public class GitLabMRCheckerFlow
    {
        // todo 处理并发
        // todo 使用消息队列

        public GitLabMRCheckerFlow(Notify notify, FileChecker fileChecker)
        {
            Notify = notify;
            FileChecker = fileChecker;
        }

        public void AddToCheck(GitLabMergeRequest gitLabMergeRequest)
        {
            var notify = Notify;
            notify.NotifyMatterMost(gitLabMergeRequest);

            var fileChecker = FileChecker;
            fileChecker.Check(gitLabMergeRequest);
        }

        public Notify Notify { get; }

        public FileChecker FileChecker { get; }
    }
}
