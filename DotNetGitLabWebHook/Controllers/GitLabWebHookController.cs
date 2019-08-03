using Microsoft.AspNetCore.Mvc;

namespace DotNetGitLabWebHook.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GitLabWebHookController : ControllerBase
    {
        [HttpPost]
        [Route("MergeRequest")]
        public IActionResult MergeRequest(object obj)
        {
            return Ok();
        }
    }
}