using DotNetGitLabWebHookToMatterMost.Model;

namespace DotNetGitLabWebHookToMatterMost.Business
{
    public class GitLabMergeRequestProvider
    {
        public GitLabMergeRequest ParseGitLabMergeRequest(GitLabMergeRequest.Rootobject rootObject)
        {
            var objectAttributes = rootObject.object_attributes;

            var sourceGitSshUrl = objectAttributes.source.git_ssh_url;
            var sourceBranch = objectAttributes.source_branch;

            // 最后提交号
            var lastCommitId = objectAttributes.last_commit.id;

            var targetGitSshUrl = objectAttributes.target.git_ssh_url;
            var targetBranch = objectAttributes.target_branch;

            // MR 标题
            var title = objectAttributes.title;

            var username = rootObject.user.username;
            var mergeRequestUrl = objectAttributes.url;

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