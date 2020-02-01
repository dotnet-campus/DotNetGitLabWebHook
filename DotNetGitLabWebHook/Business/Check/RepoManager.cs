using System.Collections.Generic;
using System.IO;
using dotnetCampus.GitCommand;
using Microsoft.Extensions.Configuration;

namespace DotNetGitLabWebHookToMatterMost.Business.Check
{
    /// <summary>
    /// 管理每个仓库对应的本地文件
    /// </summary>
    public class RepoManager
    {
        public RepoManager(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private DirectoryInfo GetRepoFolder()
        {
            var folder = Configuration["RepoFolder"];
            if (string.IsNullOrEmpty(folder))
            {
                folder = "RepoFolder";
            }

            Directory.CreateDirectory(folder);
            return new DirectoryInfo(folder);
        }

        public DirectoryInfo GetRepo(string repositoryUrl, string name)
        {
            // 找到本地仓库
            if (RepoList.TryGetValue(repositoryUrl, out var folder))
            {
                return folder;
            }

            var repoFolder = GetRepoFolder();
            folder = new DirectoryInfo(Path.Combine(repoFolder.FullName, name));
            Git.Clone(repositoryUrl, folder);
            return folder;
        }

        private Dictionary<string, DirectoryInfo> RepoList { get; } = new Dictionary<string, DirectoryInfo>();

        private IConfiguration Configuration { get; }
    }
}