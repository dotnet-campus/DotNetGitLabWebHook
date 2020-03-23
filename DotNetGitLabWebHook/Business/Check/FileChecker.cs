using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using dotnetCampus.GitCommand;
using DotNetGitLabWebHookToMatterMost.Model;

namespace DotNetGitLabWebHookToMatterMost.Business.Check
{
    public class FileChecker
    {
        public FileChecker(RepoManager repoManager)
        {
            RepoManager = repoManager;
        }

        public void Check(GitLabMergeRequest gitLabMergeRequest)
        {
            var repositoryUrl = gitLabMergeRequest.RawProperty.repository.url;
            var repoFolder = RepoManager.GetRepo(repositoryUrl, gitLabMergeRequest.RawProperty.repository.name);

            var git = new Git(repoFolder);

            git.Clean();
            git.FetchAll();

            // 切换分支
            git.Checkout(gitLabMergeRequest.CommonProperty.LastCommitId);

            var fileList = GetDiffFile(git, gitLabMergeRequest);

            foreach (var temp in FindCsprojFile(fileList,repoFolder))
            {
                var fileFormatChecker = new FileFormatChecker(temp.Value, new FileInfo(temp.Key));
                fileFormatChecker.FormatFile();
            }
        }

        private static Dictionary<string, List<FileInfo>> FindCsprojFile(List<FileInfo> fileList, DirectoryInfo repo)
        {
            var csproj = new Dictionary<string, List<FileInfo>>();

            foreach (var file in fileList)
            {
                var csprojFile = csproj.Keys.FirstOrDefault(temp =>
                    file.FullName.Contains(Path.GetDirectoryName(temp), StringComparison.OrdinalIgnoreCase));

                if (csprojFile != null)
                {
                    csproj[csprojFile].Add(file);
                }
                else
                {
                    csprojFile = FindCsprojFile(file, repo)?.FullName;

                    if (csprojFile != null)
                    {
                        csproj[csprojFile] = new List<FileInfo>()
                        {
                            file
                        };
                    }
                }
            }

            return csproj;
        }

        private static FileInfo FindCsprojFile(FileInfo file, DirectoryInfo repo)
        {
            DirectoryInfo directory = file.Directory;

            while (directory != null)
            {
                var csprojFile = directory.GetFiles("*.csproj", SearchOption.TopDirectoryOnly);

                if (csprojFile.Length > 0)
                {
                    return csprojFile[0];
                }

                if (directory == repo)
                {
                    return null;
                }

                directory = directory.Parent;
            }

            return null;
        }


        private FileInfo FileSlnFile(DirectoryInfo repoFolder)
        {
            var slnFileList = repoFolder.GetFiles("*.sln",SearchOption.TopDirectoryOnly);
            if (slnFileList.Length > 0)
            {
                return slnFileList[0];
            }

            return null;
        }

        private List<FileInfo> GetDiffFile(Git git, GitLabMergeRequest gitLabMergeRequest)
        {
            return git.DiffFile(gitLabMergeRequest.CommonProperty.TargetBranch,gitLabMergeRequest.CommonProperty.LastCommitId);
        }



        public RepoManager RepoManager { get; }
    }
}