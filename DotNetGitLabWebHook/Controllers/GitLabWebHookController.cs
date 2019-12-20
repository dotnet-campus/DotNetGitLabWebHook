using DotNetGitLabWebHook.Model;
using DotNetGitLabWebHookToMatterMost.Business;
using DotNetGitLabWebHookToMatterMost.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace DotNetGitLabWebHookToMatterMost.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GitLabWebHookController : ControllerBase
    {
        public GitLabMRCheckerFlow GitLabMrCheckerFlow { get; }

        /// <inheritdoc />
        public GitLabWebHookController(GitLabMRCheckerFlow gitLabMrCheckerFlow)
        {
            GitLabMrCheckerFlow = gitLabMrCheckerFlow;
        }

        [HttpPost]
        [Route("MergeRequest")]
        public IActionResult MergeRequest(object obj)
        {
            // 其实这是在调试 gitlab 发送的值，因为直接转换 GitLabMergeRequest 的值里面有很多不清真的

            var str = obj.ToString();

            var rootobject =
                JsonConvert.DeserializeObject<GitLabMergeRequest.Rootobject>(str);

            if (rootobject.object_kind == "merge_request")
            {
                var objectAttributes = rootobject.object_attributes;

                var sourceGitSshUrl = objectAttributes.source.git_ssh_url;
                var sourceBranch = objectAttributes.source_branch;

                // 最后提交号
                var lastCommitId = objectAttributes.last_commit.id;

                var targetGitSshUrl = objectAttributes.target.git_ssh_url;
                var targetBranch = objectAttributes.target_branch;

                // MR 标题
                var title = objectAttributes.title;

                var username = rootobject.user.username;
                var mergeRequestUrl = objectAttributes.url;

                var gitLabMergeRequest = new GitLabMergeRequest
                {
                    RawProperty = rootobject,
                    CommonProperty = new GitLabMergeRequest.MergeRequestProperty(sourceGitSshUrl,
                        sourceBranch,
                        lastCommitId,
                        targetGitSshUrl,
                        targetBranch,
                        title,
                        username,
                        mergeRequestUrl)
                };

                GitLabMrCheckerFlow.AddToCheck(gitLabMergeRequest);
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

    }
}