using DotNetGitLabWebHookToMatterMost.Model;

namespace DotNetGitLabWebHookToMatterMost.Business
{
    public class GitLabMergeRequestProvider
    {
        public GitLabMergeRequest ParseGitLabMergeRequest(GitLabMergeRequest.Rootobject rootObject)
        {
            var objectAttributes = rootObject.ObjectAttributes;

            var sourceGitSshUrl = objectAttributes.Source.GitSshUrl;
            var sourceBranch = objectAttributes.SourceBranch;

            // 最后提交号
            var lastCommitId = objectAttributes.LastCommit.Id;

            var targetGitSshUrl = objectAttributes.Target.GitSshUrl;
            var targetBranch = objectAttributes.TargetBranch;

            // MR 标题
            var title = objectAttributes.Title;

            var username = rootObject.User.Username;
            var mergeRequestUrl = objectAttributes.Url;

            var gitLabMergeRequest = new GitLabMergeRequest
            {
                RawProperty = rootObject,
                CommonProperty = new GitLabMergeRequest.MergeRequestProperty(sourceGitSshUrl,
                    sourceBranch,
                    lastCommitId,
                    targetGitSshUrl,
                    targetBranch,
                    title,
                    username,
                    mergeRequestUrl)
            };

            return gitLabMergeRequest;
        }
    }
}