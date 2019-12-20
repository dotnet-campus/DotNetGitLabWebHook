using DotNetGitLabWebHook.Model;
using DotNetGitLabWebHookToMatterMost.Model;
using Microsoft.Extensions.Configuration;

namespace DotNetGitLabWebHookToMatterMost.Business
{
    public class GitLabMRCheckerFlow
    {
        public Notify Notify { get; }
        public FileChecker FileChecker { get; }

        /// <inheritdoc />
        public GitLabMRCheckerFlow(Notify notify, FileChecker fileChecker)
        {
            Notify = notify;
            FileChecker = fileChecker;
        }

        // todo 处理并发
        // todo 使用消息队列

        public void AddToCheck(GitLabMergeRequest gitLabMergeRequest)
        {
            var notify = Notify;
            notify.NotifyMatterMost(gitLabMergeRequest);

            var fileChecker = FileChecker;
            fileChecker.Handle(gitLabMergeRequest);
        }

    }
}