using System.Text.Json;
using DotNetGitLabWebHook.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DotNetGitLabWebHook.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GitLabWebHookController : ControllerBase
    {
        [HttpPost]
        [Route("MergeRequest")]
        public IActionResult MergeRequest(GitLabMergeRequest.Rootobject obj)
        {
            if (obj.object_kind== "merge_request")
            {
                var objectAttributes = obj.object_attributes;

                var sourceGitSshUrl = objectAttributes.source.git_ssh_url;
                var sourceBranch = objectAttributes.source_branch;

                // 最后提交号
                var lastCommitId = objectAttributes.last_commit.id;

                var targetGitSshUrl = objectAttributes.target.git_ssh_url;
                var targetBranch = objectAttributes.target_branch;

                // MR 标题
                var title = objectAttributes.title;

                var username = obj.user.username;
                var mergeRequestUrl = objectAttributes.url;

                var gitLabMergeRequest = new GitLabMergeRequest
                {
                    RawProperty = obj,
                    CommonProperty = new GitLabMergeRequest.MergeRequestProperty(sourceGitSshUrl,
                        sourceBranch,
                        lastCommitId,
                        targetGitSshUrl,
                        targetBranch,
                        title,
                        username,
                        mergeRequestUrl)
                };

            }
            else
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}