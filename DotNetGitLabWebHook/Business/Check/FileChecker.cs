using System.Collections.Generic;
using System.IO;
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

            var fileList = GetDiffFile(git, gitLabMergeRequest);

            var slnFile = FileSlnFile(fileList);

            var fileFormatChecker = new FileFormatChecker(fileList,slnFile);
            fileFormatChecker.FormatFile();


        }

        private FileInfo FileSlnFile(List<FileInfo> fileList)
        {
            return new FileInfo("123.sln");
        }

        private List<FileInfo> GetDiffFile(Git git, GitLabMergeRequest gitLabMergeRequest)
        {
            return new List<FileInfo>();
        }



        public RepoManager RepoManager { get; }
    }
}