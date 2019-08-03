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
            return Ok();
        }
    }
}