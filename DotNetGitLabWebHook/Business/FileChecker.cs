using DotNetGitLabWebHookToMatterMost.Model;

namespace DotNetGitLabWebHookToMatterMost.Business
{
    /// <summary>
    /// 用于判断或修复文件清真
    /// </summary>
    public class FileChecker
    {
        public void Handle(GitLabMergeRequest gitLabMergeRequest)
        {
            // 合并到的分支
            var sourceBranch = gitLabMergeRequest.CommonProperty.SourceBranch;
            // 合并分支最后的提交
            var lastCommitId = gitLabMergeRequest.CommonProperty.LastCommitId;

            // 通过这两个值拿到修改的文件
        }
    }
}