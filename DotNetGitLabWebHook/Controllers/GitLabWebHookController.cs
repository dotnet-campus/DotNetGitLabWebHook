using System.Text.Json;
using DotNetGitLabWebHook.Business;
using DotNetGitLabWebHook.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DotNetGitLabWebHook.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GitLabWebHookController : ControllerBase
    {

        /// <inheritdoc />
        public GitLabWebHookController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        [Route("MergeRequest")]
        public IActionResult MergeRequest(object obj)
        {

            var str = obj.ToString();



            var rootobject = 
                    JsonConvert.DeserializeObject<GitLabMergeRequest.Rootobject>(str);
            if (rootobject.object_kind== "merge_request")
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

                if (GitLabMRCheckerFlow.Configuration is null)
                {
                    GitLabMRCheckerFlow.Configuration = _configuration;
                }

                GitLabMRCheckerFlow.AddToCheck(gitLabMergeRequest);
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        private readonly IConfiguration _configuration;

    }
}