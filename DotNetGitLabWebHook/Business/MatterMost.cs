using System.Net.Http;
using System.Text;

namespace DotNetGitLabWebHookToMatterMost.Business
{
    public class MatterMost
    {
        private readonly string _url;

        /// <inheritdoc />
        public MatterMost(string url)
        {
            _url = url;
        }

        public void SendText(string text)
        {
            var httpClient = new HttpClient();
            StringContent content = new StringContent("{\"text\": \"" 
                                                      + text +
                                                      "\"}", Encoding.UTF8, "application/json");
            httpClient.PostAsync(_url, content);
        }
    }
}