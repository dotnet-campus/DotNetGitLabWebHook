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
        private readonly GitLabMergeRequestProvider _gitLabMergeRequestProvider;
        public GitLabMRCheckerFlow GitLabMrCheckerFlow { get; }

        /// <inheritdoc />
        public GitLabWebHookController(GitLabMRCheckerFlow gitLabMrCheckerFlow, GitLabMergeRequestProvider gitLabMergeRequestProvider)
        {
            _gitLabMergeRequestProvider = gitLabMergeRequestProvider;
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

            if (rootobject.ObjectKind == "merge_request")
            {
                var gitLabMergeRequest = _gitLabMergeRequestProvider.ParseGitLabMergeRequest(rootobject);
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